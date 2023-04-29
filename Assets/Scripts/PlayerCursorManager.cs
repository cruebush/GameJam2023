using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCursorManager : MonoBehaviour
{

    public static PlayerCursorManager instance;
    PlayerInputManager inputSystem;
    int playerColorIndex = 0;
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
        inputSystem = GetComponent<PlayerInputManager>();
        inputSystem.enabled = true;

    }

    public void onPlayerJoin(PlayerInput player)
    {
        player.GetComponentInChildren<SpriteRenderer>().color =  PlayerManagerScript.playerManager.playerColors[playerColorIndex];
    }

    public void onCursorStart()
    {
        inputSystem.enabled = true;
        inputSystem.EnableJoining();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
