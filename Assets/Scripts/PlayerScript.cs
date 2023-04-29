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
    public GameObject cursor;
    public bool teled;
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
            if (!teled)
            {
                transform.position = new Vector3(0, 0, 0);
                rig.velocity = Vector3.zero;
                teled = true;
            }

        }
        else
        {
            teled = false;
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
        if (collision.gameObject.CompareTag("Death") && !GameManager.instance.selecting) {
            rig.transform.position = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Object") && GameManager.instance.selecting)
        {
            colliding = collision;
        }
    }

    private float horizontalState = 0;
    private float verticalState = 0;
    private Rigidbody2D? selected = null;
    private Collider2D? colliding = null;
    private Vector3 offSet = Vector3.zero;

    public void onVerticalInput(InputAction.CallbackContext context)
    {
        if (GameManager.instance.selecting)
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
    }

    public void onHorizontalInput(InputAction.CallbackContext context)
    {
        if (GameManager.instance.selecting)
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
    }

    public void onSelectInput(InputAction.CallbackContext context)
    {
        if (GameManager.instance.selecting)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (colliding != null && selected == null)
                {
                    selected = colliding.gameObject.GetComponent<Rigidbody2D>();
                    if (selected.mass == 1)
                    {
                        offSet = colliding.gameObject.transform.position - transform.position;
                        selected.mass = 2;
                    }
                    else
                    {
                        selected = null;
                    }
                }
                else if (selected != null)
                {
                    Collider2D col = selected.GetComponent<Collider2D>();
                    List<Collider2D> collisions = new List<Collider2D>();
                    col.OverlapCollider(new ContactFilter2D().NoFilter(), collisions);
                    bool placeable = true;
                    foreach (Collider2D i in collisions)
                    {
                        if (i.gameObject.CompareTag("Object"))
                        {
                            if (selected.GetComponent<ObjectScript>().type == "bomb")
                            {
                                Destroy(selected.gameObject);
                                Destroy(i.gameObject);
                            }
                            else
                            {
                                placeable = false;
                            }
                            break;
                        }

                    }
                    if (placeable)
                    {
                        selected.mass = 1;
                        selected = null;
                        offSet = Vector3.zero;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding == collision && GameManager.instance.selecting)
        {
            colliding = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.selecting)
        {
            Vector3 move = Vector3.zero;
            if (horizontalState != 0) move.x += horizontalState * Mathf.Abs(1 / horizontalState);
            if (verticalState != 0) move.y += verticalState * Mathf.Abs(1 / verticalState);
            move = move.normalized;
            transform.position += move * 0.2f;
            if (selected != null)
            {
                selected.transform.position = transform.position + offSet;
            }
        }
        else
        {


            if (thrustState == true) thrust(0.3f);
            if (shootState == true) shoot();
            if (turnState != 0) turn(turnState * Mathf.Abs(1 / turnState) * 4);
            rig.velocity *= 0.98f;
        }
    }
}
