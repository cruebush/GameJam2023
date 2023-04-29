using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIMeteor : MonoBehaviour
{
    public UnityEvent action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void loadMainScene()
    {
        if(PlayerManagerScript.playerManager.playerList.Count > 1)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            StartCoroutine(GameManager.instance.showText());
        }
    }

    public void whenthatonescenechanges()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, .1f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            action?.Invoke();
        }
    }
}
