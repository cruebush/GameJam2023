using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    public Transform target;
    public int turnMin;
    public int turnMax;
    public float turnSpeed;
    public int bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turn();
    }

    private void turn()
    {
        // Rotate the turret
        Vector3 direction = (target.position - transform.position);
        /*Quaternion toRotation = Quaternion.FromToRotation(transform.position, direction); // instead of LookRotation( )
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * turnSpeed);*/

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
}
