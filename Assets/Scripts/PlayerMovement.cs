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

    private Vector3 moveDir;
    void Start()
    {
        charCtrlr = GetComponent<CharacterController>();
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(Input.GetAxis("Vertical")) >= inputFilter &&  charCtrlr.velocity.magnitude < maxSpeed)
        {
            moveDir.z +=  Input.GetAxis("Vertical") * speed * Time.deltaTime;
        }
        else if (moveDir.z > 0.001 || moveDir.z < -0.001)
        {
            moveDir.z -= moveDir.z * friction * Time.deltaTime;
        }
        else
        {
            moveDir.z = 0;
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) >= inputFilter &&  charCtrlr.velocity.magnitude < maxSpeed)
        {
            moveDir.x += speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else if (moveDir.x > 0.001 || moveDir.x < -0.001)
        {
            moveDir.x -= moveDir.x * friction  * Time.deltaTime;
        }
        else
        {
            moveDir.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && charCtrlr.isGrounded)
        {
            moveDir.y += 1f;
        }
        else //if (!charCtrlr.isGrounded)
        {
           moveDir.y += -0.5f * Time.deltaTime; 
        }

        moveDir =  moveDir ;
        
        charCtrlr.Move(transform.rotation *moveDir);
        
    }
}
