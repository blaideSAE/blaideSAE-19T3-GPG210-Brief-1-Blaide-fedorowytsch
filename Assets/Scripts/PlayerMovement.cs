using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController charCtrlr;
    private Rigidbody rB;

    public float inputFilter;
    public float speed;
    public float friction;
    public float maxSpeed;

    private Vector3 velocity;
    private Vector3 moveDir;
    public float jumpForce;

    void Start()
    {
        charCtrlr = GetComponent<CharacterController>();
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            moveDir.z = Input.GetAxis("Vertical");
            moveDir.x = Input.GetAxis("Horizontal");
            moveDir = transform.rotation * moveDir;
            velocity += moveDir * (charCtrlr.isGrounded? 1:0.5f)* speed * Time.deltaTime;
            
        if(charCtrlr.isGrounded)
        {
            velocity.x -= velocity.normalized.x * Time.deltaTime *1;
            velocity.z -= velocity.normalized.z *Time.deltaTime *1;
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && charCtrlr.isGrounded)
        {
            velocity.y = jumpForce;
        }
        else if (!charCtrlr.isGrounded)
        {
           velocity.y += -0.5f * Time.deltaTime; 
        }

        charCtrlr.Move(velocity);
    }
}
