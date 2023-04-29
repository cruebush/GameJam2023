using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rig;
    private bool thrustState = false;
    private bool shootState = false;
    private float turnState = 0;
    private int shootDelay = 70;

    public BulletScript bulletPrefab;

    void thrust(float power)
    {
        if (!GameManager.instance.selecting)
        {
            float theta = Mathf.Deg2Rad * rig.transform.rotation.eulerAngles.z;
            rig.velocity += power * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
        }
    }

    void turn(float dir)
    {
        if (!GameManager.instance.selecting)
        {
            rig.transform.Rotate(new Vector3(0, 0, dir));
        }
    }

    void shoot() {
        if (!GameManager.instance.selecting)
        {
            if (shootDelay <= 0)
            {
                shootDelay = 70;
                BulletScript bul = Instantiate(bulletPrefab);
                bul.launch(rig.transform.position, Mathf.Deg2Rad * rig.transform.rotation.eulerAngles.z, 12);
            }
        }
    }

    private void Update()
    {
        shootDelay--;
        if (GameManager.instance.selecting)
        {
            transform.position = new Vector3(0, 0, 0);
        }
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
        /*if (context.phase == InputActionPhase.Started)
        {
            turnState = context.ReadValue<float>();
        }*/
        turnState = context.ReadValue<float>();
        if (context.phase == InputActionPhase.Canceled) {
            turnState = 0;
        }
    }

    public void onShootInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            shootState = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            shootState = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death")) {
            rig.transform.position = Vector2.zero;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (thrustState == true) thrust(0.3f);
        if (shootState == true) shoot();
        if (turnState != 0) turn(turnState * Mathf.Abs(1/turnState) * 4);
        rig.velocity *= 0.98f;
    }
}
