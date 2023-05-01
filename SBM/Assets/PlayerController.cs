using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("References")]
    public Rigidbody rb;
    public Transform head;
    public Camera camera;



    [Header("Configurations")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;



    [Header("Runtime")]
    Vector3 newVelocity;
    bool isGrounded = false;
    bool isJumping = false;

    [Header("Wwise Events")]
    public AK.Wwise.Event myFootstep;

    //Wwise
    private bool audioIsPlaying = false;
    private float lastFootstepTime = 0;

    // Start is called before the first frame update
    // set cursor to not be visible
    // set lockState

    void Start() {
        //  Hide and lock the mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        lastFootstepTime = Time.time;

    }




    // Update is called once per frame
    void Update() {
        // Horizontal rotation
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);   // Adjust the multiplier for different rotation speed

        // changing the velocity and assigning the new velocity to variable newVelocity
        newVelocity = Vector3.up * rb.velocity.y;
        // If the player presses shift to sprint, set speed to running, else set speed to walking
        float speed = walkSpeed;
        // horizontal velocity is the speed we decided above (variable speed)
        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        // vertical velocity is the speed we decided above (variable speed)
        newVelocity.z = Input.GetAxis("Vertical") * speed;

        // if the player is on the ground
        if (isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
                newVelocity.y = jumpSpeed;
                isJumping = true;
            }

            else

            {
                isJumping = false;
            }
        }
        rb.velocity = transform.TransformDirection(newVelocity);
        playFootstepsAudio();

    }
    //the function that I MATTHEW KASE inserted, not used anywhere in the script right now.
    private void playFootstepsAudio()
    {
        float playerSpeedX = rb.velocity.x;
        float playerSpeedZ = rb.velocity.z;
        if (isGrounded && !isJumping && (playerSpeedX != 0 || playerSpeedZ != 0))
        {
            //check to see if audio is not currently playing
            if (!audioIsPlaying)
            {
                myFootstep.Post(gameObject);
                lastFootstepTime = Time.time;
                audioIsPlaying = true;
            }

            else

            {
                if (Time.time - lastFootstepTime > 0.5)
                {
                    audioIsPlaying = false;
                }
            }

        } else
        {
            return;
        }

    }

    void FixedUpdate() {
        //  Shoot a ray of 1 unit towards the ground
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f)) {
            isGrounded = true;
        }
        else isGrounded = false;
    }


    void LateUpdate() {
        // Vertical rotation
        Vector3 e = head.eulerAngles;
        e.x -= Input.GetAxis("Mouse Y") * 2f;   //  Edit the multiplier to adjust the rotate speed
        e.x = RestrictAngle(e.x, -85f, 85f);    //  This is clamped to 85 degrees. You may edit this.
        head.eulerAngles = e;
    }




    //  This will be called constantly
    void OnCollisionStay(Collision col) {
        if (Vector3.Dot(col.GetContact(0).normal, Vector3.up) <= .6f)
            return;

        isGrounded = true;
        isJumping = false;
    }


    void OnCollisionExit(Collision col) {
        isGrounded = false;
    }




    //  A helper function
    //  Clamp the vertical head rotation (prevent bending backwards)
    public static float RestrictAngle(float angle, float angleMin, float angleMax) {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        if (angle > angleMax)
            angle = angleMax;
        if (angle < angleMin)
            angle = angleMin;

        return angle;
    }
}
