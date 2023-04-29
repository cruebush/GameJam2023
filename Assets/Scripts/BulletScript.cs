using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody2D rig;

    public void launch(Vector2 pos, float theta, float speed)
    {
        rig.transform.position = pos + new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * 0.5f;
        rig.velocity = speed * new Vector2 (Mathf.Cos(theta), Mathf.Sin(theta));
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
