using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    TextMeshProUGUI text;
    public static GameManager instance;
    public bool selecting;
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        selecting = false;

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            text = GameObject.FindGameObjectWithTag("HelpText").GetComponent<TextMeshProUGUI>();
            text.text = "";
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    public void onSceneLoaded(Scene scene, LoadSceneMode lsm)
    {
        if(scene.name == "TitleScreen")
        {
            text = GameObject.FindGameObjectWithTag("HelpText").GetComponent<TextMeshProUGUI>();
            text.text = "";
		audioSource.clip = audioClips[0];
        }
	  else if(scene.name == "CreditScreen"){
		audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else
	  {
		audioSource.clip = audioClips[1];
            audioSource.Play();
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

    public IEnumerator showText()
    {
        text.text = "You need at least two players to play!";
        yield return new WaitForSeconds(5);
        text.text = "";
    }
}
