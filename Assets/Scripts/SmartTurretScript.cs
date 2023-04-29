using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartTurretScript : MonoBehaviour
{
    public List<Transform> allTargets;
    public Transform target;
    public float turnMin;
    public float turnMax;
    public float turnSpeed;
    public int bulletSpeed;
    public BulletScript bulletPrefab;
    public bool canFire;
    public float fireRate;
    public Rigidbody2D targetRig;
    public float distance;
    public bool routineStarted;





    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.selecting)
        {
            allTargets = new List<Transform>();
            foreach (PlayerScript player in PlayerManagerScript.playerManager.playerList)
            {
                allTargets.Add(player.transform);
            }
            pickTarget();
            turn();
            canFire = true;
            if (!routineStarted)
            {
                routineStarted = true;
                StartCoroutine(fire());
            }
        }
        else
        {
            canFire = false;
        }
    }

    private void pickTarget()
    {
        List<Transform> newTargets = new List<Transform>();
        foreach (Transform t in allTargets)
        {
            Vector3 direction = (t.position - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (rotation.z > turnMin && rotation.z < turnMax)
            {
                newTargets.Add(t);
            }
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

    private void turn()
    {
        // Rotate the turret
        Vector3 direction = (target.position - transform.position + new Vector3(targetRig.velocity.x * Vector3.Distance(transform.position, target.position)/ bulletSpeed * 1, targetRig.velocity.y * Vector3.Distance(transform.position, target.position) / bulletSpeed * 1, 0));
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
            if (!canFire)
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
