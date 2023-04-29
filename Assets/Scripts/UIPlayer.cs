using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIPlayer : MonoBehaviour
{
    public int lowerx;
    public int upperx;
    public int lowery;
    public int uppery;
    List<Transform> transforms = new List<Transform>();

    public void addPlayer(PlayerInput player)
    {
        transforms.Add(player.transform);
    }

    public void removePlayer(PlayerInput player)
    {
        transforms.Remove(player.transform);
    }

    private void Update()
    {
        foreach(Transform t in transforms)
        {
            if (t.position.x < lowerx)
            {
                t.position = new Vector2(upperx, t.position.y);
            }
            if (t.position.x > upperx)
            {
                t.position = new Vector2(lowerx, t.position.y);
            }
            if (t.position.y < lowery)
            {
                t.position = new Vector2(t.position.x, uppery);
            }
            if (t.position.y > uppery)
            {
                t.position = new Vector2(t.position.x, lowery);
            }
        }

    }
}
