using System;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    //PhotonView
    private PhotonView PV;

    //Rigidbody
    public Rigidbody rb;

    //Camera
    public Transform cam;

    public float camRotSpeed = 5f;
    public float camMinY = -60f;
    public float camMaxY = 75f;
    public float rotSmoothSpeed = 10f;

    public float walkSpeed = 9f;
    public float runSpeed = 14f;
    public float maxSpeed = 20f;
    public float jumpPower = 50f;

    //Extra Grav
    public float extraGrav = 1f;

    float bodyRotX;
    float camRotY;
    Vector3 directionIntentX;
    Vector3 directionIntentY;
    float speed;

    //isGrounded?
    public bool grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            lookRotation();
            Movement();
            ExtraGravity();
            groundCheck();
            if (grounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    void lookRotation()
    {
        bodyRotX += Input.GetAxis("Mouse X") * camRotSpeed;
        camRotY += Input.GetAxis("Mouse Y") * camRotSpeed;

        //clamp
        camRotY = Mathf.Clamp(camRotY, camMinY, camMaxY);

        //rot targ and hand rot
        Quaternion camTargRot = Quaternion.Euler(-camRotY, 0, 0);
        Quaternion bodyTargRot = Quaternion.Euler(0, bodyRotX, 0);

        //handle rot
        transform.rotation = Quaternion.Lerp(transform.rotation, bodyTargRot, Time.deltaTime * rotSmoothSpeed);
        cam.localRotation = Quaternion.Lerp(cam.localRotation, camTargRot, Time.deltaTime * rotSmoothSpeed);
    }

    void Movement()
    {
        //direction match cam
        directionIntentX = cam.right;
        directionIntentX.y = 0;
        directionIntentX.Normalize();

        directionIntentY = cam.forward;
        directionIntentY.y = 0;
        directionIntentY.Normalize();

        rb.velocity = directionIntentY * Input.GetAxis("Vertical") * speed + directionIntentX * Input.GetAxis("Horizontal") * speed + Vector3.up * rb.velocity.y;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        //speed based on move state
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = runSpeed;
        }

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            speed = walkSpeed;
        }

    }

    void ExtraGravity()
    {
        if (grounded)
        {
            rb.AddForce(Vector3.down * extraGrav);
        }
    }

    void groundCheck()
    {
        RaycastHit groundhit;
        grounded = Physics.Raycast(transform.position, -transform.up, out groundhit, 1.25f);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
