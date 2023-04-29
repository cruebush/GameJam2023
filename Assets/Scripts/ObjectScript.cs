using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectScript : MonoBehaviour
{
    public string type;
    public BulletScript bulletPrefab;
    private int shootDelay = 0;

    void shoot(float theta, float speed) {
        BulletScript bullet = Instantiate(bulletPrefab);
        bullet.launch(transform.position, theta, speed);
    }

    void octo()
    {
        if (shootDelay <= 0) {
            shootDelay = 70;
            for(int i = 0; i < 360; i+=45)
            {
                shoot((Mathf.Deg2Rad) * (transform.rotation.eulerAngles.z + 90 + i), 8);
            }
        }
    }

    void pulser()
    {
        if (shootDelay <= 0)
        {
            shootDelay = 400;
            for (int i = 0; i < 360; i += 9)
            {
                shoot((Mathf.Deg2Rad) * (transform.rotation.eulerAngles.z + 90 + i), 3);
            }
        }
    }

    void spinner()
    {
        transform.Rotate(new Vector3(0, 0, 1));
        if (shootDelay <= 0)
        {
            shootDelay = 15;
            shoot((Mathf.Deg2Rad) * (transform.rotation.eulerAngles.z + 90), 6);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.selecting)
        {
            shootDelay--;
            switch (type)
            {
                case "octo":
                    octo();
                    break;
                case "spinner":
                    spinner();
                    break;
                case "pulser":
                    pulser();
                    break;
            }
        }
    }
}
