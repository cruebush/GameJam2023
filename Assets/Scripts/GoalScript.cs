using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    
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
    }
}
