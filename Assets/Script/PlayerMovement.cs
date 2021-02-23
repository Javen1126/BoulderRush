using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce;
    public float sideForce;
    public float jumpForce;
    public float jumpHeight;
    private Vector3 jump;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        jump = new Vector3(0f, jumpHeight, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //better to use this when calculating physics
    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime); //Add forward force to z axis

        transform.Translate(Input.acceleration.x * Time.smoothDeltaTime * 20f, 0, 0); // Accelerometer to tilt left and right

        //Add force to x axis to control movement left and right (test movement using keyboard)
        if (Input.GetKey("right"))
        {
            rb.AddForce(sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("left"))
        {
            rb.AddForce(-sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if ((Input.GetKey("up") && isGrounded) || (Input.GetMouseButton(0) && isGrounded) && (!EventSystem.current.IsPointerOverGameObject() || !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (rb.position.y < -1f)
        {
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
            FindObjectOfType<GameManager>().GameOver();
        }
            
    }

}
