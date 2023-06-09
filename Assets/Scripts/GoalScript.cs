using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    bool finishedWinAnim;
    bool hasCursors;
    float timerMax = 10;
    float timer = 0;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Entering State");
            StartCoroutine(winState());
        }
    }

    public IEnumerator winState()
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.selecting = true;
        GameObject[] objects = Resources.LoadAll<GameObject>("");

        for (int i = 0; i < PlayerManagerScript.playerManager.playerList.Count; i++)
        {
            GameObject x = Instantiate(objects[Random.Range(0, objects.Length - 1)]);
            x.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        }

        finishedWinAnim = true;
        hasCursors = true;
        while (!finishedWinAnim)
        {
            yield return new WaitForSeconds(1);
        }



        PlayerCursorManager.instance.onCursorStart();
        while (hasCursors)
        {
            yield return new WaitForSeconds(1);
        }
        GameManager.instance.selecting = false;
    }

    private void Update()
    {
        if (GameManager.instance.selecting)
        {
            timer += Time.deltaTime;
            if (timer > timerMax)
            {
                timer = 0;
                GameManager.instance.selecting = false;
            }
        }
    }
}
