using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    bool finishedWinAnim;
    bool hasCursors;
    
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
}