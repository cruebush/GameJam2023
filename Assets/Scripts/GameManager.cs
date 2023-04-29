using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void quit()
    {
        Application.Quit();
    }
    public void loadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void onEscapeKeyPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Escape Key pressed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
