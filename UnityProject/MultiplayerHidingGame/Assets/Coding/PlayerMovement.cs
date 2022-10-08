using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Transform orientation;

    public float groundDrag;
    float hMove;
    float zMove;

    public float playerHeight;
    public LayerMask ground;
    private bool grounded;

    Vector3 moveDir;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        hMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");

        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        if (grounded)
        {
            rb.drag = groundDrag;
        }else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        moveDir = orientation.forward * zMove + orientation.right * hMove;
        rb.AddForce(moveDir.normalized * speed * 10f, ForceMode.Force);
    }
}
