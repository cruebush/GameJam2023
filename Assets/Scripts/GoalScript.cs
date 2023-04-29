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
        GameManager.instance.selecting = true;
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
