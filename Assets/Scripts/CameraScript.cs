using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera cam;
    private PlayerManagerScript management;

    // Start is called before the first frame update
    void Start()
    {
        management = FindFirstObjectByType<PlayerManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        print(management.playerList.Count);
    }
}
