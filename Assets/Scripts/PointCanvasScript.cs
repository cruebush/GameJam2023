using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointCanvasScript : MonoBehaviour
{
    public int wins;
    public Image[] images;
    public Color playerColor;

    // Start is called before the first frame update
    void Start()
    {
        playerColor = GetComponentInParent<PlayerScript>().pColor;
        SceneManager.sceneLoaded += onSceneLoaded;
       
    }

    public void onSceneLoaded(Scene scene, LoadSceneMode lsm)
    {
        if(scene.name == "Level2")
        {
            int index = PlayerManagerScript.playerManager.playerList.IndexOf(GetComponentInParent<PlayerScript>());
            Camera[] camera = FindObjectsByType<Camera>(FindObjectsSortMode.None);
            Camera mCamera = null;
            for(int i = 0; i < camera.Length; i++)
            {
                if(camera[i].GetComponent<CameraFollowScript>().camIndex == index)
                {
                    mCamera = camera[i];
                }
                
            }
            Debug.Log(mCamera);
            GetComponent<Canvas>().worldCamera = mCamera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addpoint()
    {
        wins++;
        for(int i = 0; i < images.Length; i++)
        {
            if(wins > i)
            {
                images[i].color = playerColor;
            }
        }
        if(wins > 4)
        {
            GameManager.instance.loadNewScene("TitleScreen");
        }
    }
}
