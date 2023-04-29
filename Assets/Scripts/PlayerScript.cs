using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rig;
    private bool thrustState = false;
    private float turnState = 0;

    public BulletScript bulletPrefab;

    void thrust(float power)
    {
        float theta = Mathf.Deg2Rad * rig.transform.rotation.eulerAngles.z;
        rig.velocity += power * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }

    void turn(float dir)
    {
        rig.transform.Rotate(new Vector3(0, 0, dir));
    }

    void shoot() { 
        BulletScript bul = Instantiate(bulletPrefab);
        bul.launch(rig.transform.position, Mathf.Deg2Rad * rig.transform.rotation.eulerAngles.z, 3);
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
            turnState = context.ReadValue<float>();
        }
        else if(context.phase == InputActionPhase.Canceled) {
            turnState = 0;
        }
    }

    public void onShootInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            shoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (thrustState == true) thrust(0.1f);
        if (turnState != 0) turn(turnState * 0.7f);
        rig.velocity *= 0.993f;
    }
}
