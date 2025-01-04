using UnityEngine;

public class FireShell : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;

    private float speed = 15.0f;
    private float rotationSpeed = 5.0f;
    private float moveSpeed = 1.0f;

    private static float delayReset = 0.2f;
    private float delay = delayReset;

    private void Update()
    {
        delay -= Time.deltaTime;
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        
        float? angle = RotateTurret();

        if (angle != null && delay <= 0.0f)
        {
            CreateBullet();
            delay = delayReset;
        }
        else
        {
            transform.Translate(0.0f, 0.0f, Time.deltaTime * moveSpeed);
        }
    }

    private void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().linearVelocity = speed * turretBase.forward;
    }

    private float? RotateTurret()
    {
        float? angle = CalculateAngle(false);

        if (angle != null)
        {
            turretBase.localEulerAngles = new Vector3(360.0f - (float)angle, 0.0f, 0.0f);
        }
        return angle;
    }

    private float? CalculateAngle(bool returnLowAngle)
    {
        Vector3 targetDir = enemy.transform.position - transform.position;
        float y = targetDir.y;
        targetDir.y = 0.0f;
        float x = targetDir.magnitude - 1.0f;
        float gravity = 9.8f;
        float speedSqr = speed * speed;
        float underTheSqrRoot = (speedSqr * speedSqr) - gravity * (gravity * x * x + 2 * y * speedSqr);

        if (underTheSqrRoot >= 0.0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = speedSqr + root;
            float lowAngle = speedSqr - root;

            if (returnLowAngle)
            {
                // low angle
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                // high angle
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
        {
            return null;
        }
    }

    //Vector3 CalculateTrajectory() {
    //    Vector3 p = enemy.transform.position - this.transform.position;
    //    Vector3 v = enemy.transform.forward * enemy.GetComponent<Drive>().speed;
    //    float s = bullet.GetComponent<MoveShell>().speed;

    //    float a = Vector3.Dot(v, v) - s * s;
    //    float b = Vector3.Dot(p, v);
    //    float c = Vector3.Dot(p, p);
    //    float d = b * b - a * c;

    //    if (d < 0.1f) return Vector3.zero;

    //    float sqrt = Mathf.Sqrt(d);
    //    float t1 = (-b - sqrt) / c;
    //    float t2 = (-b + sqrt) / c;

    //    float t = 0.0f;
    //    if (t1 < 0.0f && t2 < 0.0f) return Vector3.zero;
    //    else if (t1 < 0.0f) t = t2;
    //    else if (t2 < 0.0f) t = t1;
    //    else {
    //        t = Mathf.Max(new float[] { t1, t2 });
    //    }
    //    return t * p + v;
    //}
}