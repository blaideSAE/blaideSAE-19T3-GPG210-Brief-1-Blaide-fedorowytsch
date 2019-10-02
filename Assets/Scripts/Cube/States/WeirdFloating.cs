using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdFloating : StateBase
{
    private Rigidbody rB;
    public float dirChngeSpeed,speed;
    public float timer;
    public float timeLimit;
    private float randX, randY, randZ, startY;
    
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        
    }
    public override void Enter()
    {
        rB.useGravity = false;
        rB.velocity = Vector3.zero;
        timer = 0;
        randX = Random.Range(-1f, 1f);
        randY = Random.Range(-1f, 1f);
        randZ = Random.Range(-1f, 1f);
        startY = transform.position.y;
    }
    public override void Execute()
    {
        if (timer < timeLimit)
        {
            float x =  Mathf.PerlinNoise(Time.time * dirChngeSpeed * randX, 0).Remap(0,1,-0.5f,0.5f);
            float y =  Mathf.PerlinNoise(Time.time * dirChngeSpeed * randY, 0).Remap(0,1,-0.5f,0.5f);
            float z =  Mathf.PerlinNoise(Time.time * dirChngeSpeed * randZ, 0).Remap(0,1,-0.5f,0.5f);
            
            Debug.Log(x +" ," + y + " ," + z);
            //rB.AddForce(x*speed,y*speed,z*speed);
            rB.velocity = new Vector3(x,y,z) *speed;
            rB.angularVelocity = new Vector3(z, x, y) * speed;
            //transform.position = new Vector3(x, y + startY, z);
            timer += Time.deltaTime;
        }
        else
        {
            sM.ChangeState(nextState);
        }

    }

    public override void Exit()
    {
        rB.useGravity = true;
    }
}
