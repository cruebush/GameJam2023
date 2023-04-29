using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerManagerScript : MonoBehaviour
{
    public List<PlayerScript> playerList;
    public static PlayerManagerScript playerManager;
    public PlayerInputManager playerInputManager;
    public Color[] playerColors;

    // Start is called before the first frame update
    void Start()
    {
        if(playerManager == null)
        {
            playerManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }


    public void onPlayerJoin(PlayerInput player)
    {
        DontDestroyOnLoad(player);
        if (player != null && player.tag == "Player")
        {
            Debug.Log("player joined");
           
            playerList.Add(player.GetComponent<PlayerScript>());
            
            player.GetComponentInChildren<SpriteRenderer>().color = playerColors[playerList.Count - 1];
            }
            
        }

    }
