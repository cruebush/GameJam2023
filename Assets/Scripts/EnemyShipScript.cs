using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScript : MonoBehaviour
{
    public Rigidbody2D rig;
    private bool thrustState = false;
    private bool shootState = false;
    private float turnState = 0;
    private int shootDelay = 70;
    public List<Transform> allTargets;
    public Transform target;
    public float turnSpeed;
    public int bulletSpeed;
    public BulletScript bulletPrefab;
    public bool canFire;
    public float fireRate;
    public Rigidbody2D targetRig;
    public float thrustPower;
    public float fireMin;
    public float fireMax;

    // Start is called before the first frame update

    void thrust(float power)
    {
        Vector3 direction = (target.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        print(transform.rotation.y - rotation.y);
        if (Mathf.Abs(transform.rotation.z - rotation.z) > fireMin)
        {
            canFire = false;
        }
        else
        {
            float theta = Mathf.Deg2Rad * (rig.transform.rotation.eulerAngles.z + 90);
            rig.velocity += thrustPower * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
            if (!canFire)
            {
                canFire = true;
                StartCoroutine(fire());
            }
        }
    }

    void Update()
    {
        pickTarget();
        turn();
        thrust(thrustPower);
    }

    private void turn()
    {
        Vector3 direction = (target.position - transform.position + new Vector3(targetRig.velocity.x * Vector3.Distance(transform.position, target.position) / bulletSpeed * .2f, targetRig.velocity.y * Vector3.Distance(transform.position, target.position) / bulletSpeed * .2f, 0));
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Debug.Log(rotation.z);

        if (!canFire)
        {
            canFire = true;
            StartCoroutine(fire());
        }
        
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    private void pickTarget()
    {
        List<Transform> newTargets = new List<Transform>();
        foreach (Transform t in allTargets)
        {
            Vector3 direction = (t.position - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

               newTargets.Add(t);
        }

        if (newTargets.Count > 0)
        {
            float? minDistance = null;
            foreach (Transform t in newTargets)
            {
                float distance = Vector3.Distance(transform.position, t.position);
                minDistance = (minDistance == null) ? distance : (distance < minDistance) ? distance : minDistance;
                if (distance == minDistance)
                {
                    target = t;
                    targetRig = target.GetComponentInParent<Rigidbody2D>();
                }
            }
        }
    }

    private IEnumerator fire()
    {
        yield return new WaitForSeconds(fireRate);

        while (canFire)
        {
            BulletScript bullet = Instantiate(bulletPrefab);
            bullet.launch(transform.position, (Mathf.Deg2Rad) * (transform.rotation.eulerAngles.z + 90), bulletSpeed);
            yield return new WaitForSeconds(fireRate);
        }
    }
}
