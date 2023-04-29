using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    bool finishedWinAnim;
    bool hasCursors;
    
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            StartCoroutine(winState());
        }
    }

    public IEnumerator winState()
    {
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
    }
}
