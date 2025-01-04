using UnityEngine;

public class AIShell : MonoBehaviour
{
    public GameObject explosion;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.forward = rb.linearVelocity;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            Debug.Log("Hit tank");
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(gameObject);
        }
    }
}