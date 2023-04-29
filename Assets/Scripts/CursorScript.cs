using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorScript : MonoBehaviour
{

    private float horizontalState = 0;
    private float verticalState = 0;
    private Rigidbody2D? selected = null;
    private Collider2D? colliding = null;
    private Vector3 offSet = Vector3.zero;

    public void onVerticalInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            verticalState = context.ReadValue<float>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            verticalState = 0;
        }
    }

    public void onHorizontalInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            horizontalState = context.ReadValue<float>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            horizontalState = 0;
        }
    }

    public void onSelectInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (colliding != null && selected == null) {
                selected = colliding.gameObject.GetComponent<Rigidbody2D>();
                if (selected.mass == 1)
                {
                    offSet = colliding.gameObject.transform.position - transform.position;
                    selected.mass = 2;
                }
                else {
                    selected = null;
                }
            } else if (selected != null) {
                selected.mass = 1;
                selected = null;
                offSet = Vector3.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object")) {
            colliding = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding == collision)
        {
            colliding = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        if (horizontalState != 0) move.x += horizontalState * Mathf.Abs(1 / horizontalState);
        if (verticalState != 0) move.y += verticalState * Mathf.Abs(1 / verticalState);
        move = move.normalized;
        transform.position += move * 0.02f;
        if (selected != null) {
            selected.transform.position = transform.position + offSet;
        }
    }
}
