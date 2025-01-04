using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    private float speed = 0.0f;
    private float ySpeed = 0.0f;
    private float mass = 30.0f;
    private float force = 4.0f;
    private float drag = 1.0f;
    private float gravity = -9.8f;
    private float gAccel;
    private float acceleration;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        acceleration = force / mass;
        speed += acceleration * 1.0f;
        gAccel = gravity / mass;
    }

    private void LateUpdate()
    {
        speed *= (1 - Time.deltaTime * drag);
        ySpeed += gAccel * Time.deltaTime;
        transform.Translate(0.0f, ySpeed, speed);
    }
}