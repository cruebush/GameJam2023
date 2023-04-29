using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public int camIndex;
    public Transform target;
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null && PlayerManagerScript.playerManager.playerList.Count >= camIndex + 1)
        {
            target = PlayerManagerScript.playerManager.playerList[camIndex].transform;
        }

        
        if (target != null)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(target.position.x, target.position.y, -10), Time.deltaTime * 5);
            //cameraTransform.position = target.position;
            print("test");
        }
    }
}
