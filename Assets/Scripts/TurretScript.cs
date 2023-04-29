using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public List<Transform> allTargets;
    public Transform target;
    public float turnMin;
    public float turnMax;
    public float turnSpeed;
    public int bulletSpeed;
    public BulletScript bulletPrefab;
    private bool canFire;
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fire());
    }

    // Update is called once per frame
    void Update()
    {
        pickTarget();
        turn();
    }

    private void pickTarget()
    {
        List<Transform> newTargets = new List<Transform>();
        foreach(Transform t in allTargets)
        {
            Vector3 direction = (t.position - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (rotation.z > turnMin && rotation.z < turnMax)
            {
                newTargets.Add(t);
            }
        }

        if(newTargets.Count > 0)
        {
            float? minDistance = null;
            foreach(Transform t in newTargets)
            {
                float distance = Vector3.Distance(transform.position, t.position);
                minDistance = (minDistance == null) ? distance : (distance < minDistance) ? distance : minDistance;
                if(distance == minDistance)
                {
                    target = t;
                }
            }
        }
    }

    private void turn()
    {
        // Rotate the turret
        Vector3 direction = (target.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Debug.Log(rotation.z);
        if (rotation.z < turnMin)
        {
            rotation = Quaternion.Euler(new Vector3(0, 0, turnMin));
            canFire = false;
        }
        else if (rotation.z > turnMax)
        {
            rotation = Quaternion.Euler(new Vector3(0, 0, turnMin));
            canFire = false;
        }
        else
        {
            if(!canFire)
            {
                canFire = true;
                StartCoroutine(fire());
            }
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
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
