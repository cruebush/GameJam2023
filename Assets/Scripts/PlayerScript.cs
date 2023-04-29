using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rig;
    private bool thrustState = false;
    private int turnState = 0;

    void thrust(float power)
    {
        float theta = Mathf.Deg2Rad * rig.transform.rotation.eulerAngles.z;
        rig.velocity += power * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }

    void turn(float dir)
    {
        rig.transform.Rotate(new Vector3(0, 0, dir));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            thrustState = true;
        } else if (context.phase == InputActionPhase.Canceled) {
            thrustState = false;
        }
    }
    public void onTurnInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            turnState = context.ReadValue<int>();
        }
        else if(context.phase == InputActionPhase.Canceled) {
            turnState = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (thrustState == true) thrust(0.1f);
        if (turnState == 0) turn(turnState);
    }
}
