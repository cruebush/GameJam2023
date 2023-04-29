using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    public Transform target;
    public float turnMin;
    public float turnMax;
    public float turnSpeed;
    public int bulletSpeed;
    public BulletScript bulletPrefab;
    public bool canFire;
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fire());
    }

    // Update is called once per frame
    void Update()
    {
        turn();
    }

    private void turn()
    {
        // Rotate the turret
        canFire = true;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    private IEnumerator fire()
    {
        while (true)
        {
            if (canFire)
            {
                BulletScript bullet = Instantiate(bulletPrefab);
                bullet.launch(transform.position, Mathf.Deg2Rad * transform.rotation.eulerAngles.z, bulletSpeed);
                yield return new WaitForSeconds(fireRate);
            }

        }
    }
}
