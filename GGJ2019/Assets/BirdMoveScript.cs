using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMoveScript : MonoBehaviour {

    private float cylinderX;
    private float cylinderZ;
    static private float rotAngle;
    private bool hasJumped;
    private bool isFalling;
    Rigidbody birdBody;
    
	// Use this for initialization
	void Start () {
        cylinderX = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.x;
        cylinderZ = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.z;
        birdBody = gameObject.GetComponent<Rigidbody>();
#if UNITY_EDITOR
      //  Debug.Log("XZ (" + cylinderX + ", " + cylinderZ + ")");
#endif
        rotAngle = 0.0f;
        hasJumped = false;
        isFalling = false;
    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime, Space.World);
#if UNITY_EDITOR
            // Debug.Log("AWAY");
#endif
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotAngle -= 30.0f * Time.deltaTime;
#if UNITY_EDITOR
           // Debug.Log("LEFT");
#endif
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotAngle += 30.0f * Time.deltaTime;
#if UNITY_EDITOR
           // Debug.Log("RIGHT");
#endif
        }

        gameObject.transform.position = new Vector3(5 * Mathf.Sin(Mathf.Deg2Rad * rotAngle), gameObject.transform.position.y, 5 * -Mathf.Cos(Mathf.Deg2Rad * rotAngle));

        if (rotAngle < 0)
        {
            rotAngle += 360;
        }
        else if(rotAngle > 360)
        {
            rotAngle -= 360;
        }
       

       // Debug.Log("rotAngle: " + rotAngle);

        //Check for isFalling
        if(hasJumped && birdBody.velocity.y < 0 && !isFalling)
        {
            //Debug.Log("Fall trigger");
            isFalling = true;
        }


        if(Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            hasJumped = true;
            birdBody.velocity = new Vector3(birdBody.velocity.x, 8.0f, birdBody.velocity.z);
        }

        //Hold space while falling to slow the fall
        if(isFalling && Input.GetKey(KeyCode.Space))
        {
            
            birdBody.velocity += new Vector3(0.0f, 7.0f * Time.deltaTime, 0.0f);
          //  Debug.Log("Fall mitigation, velocity: " + birdBody.velocity.y);
        }

        gameObject.transform.LookAt(new Vector3(GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.x, this.transform.position.y, GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.z));

#if UNITY_EDITOR
        //Debug.Log("(" + Mathf.Sin(Mathf.Deg2Rad * rotAngle) + ", " + gameObject.transform.position.y + ", " + -Mathf.Cos(Mathf.Deg2Rad * rotAngle) +  ")");
#endif
    }

    public void OnCollisionEnter(Collision collision)
    {
        hasJumped = false;
        isFalling = false;
    }

    static public float GetRotAngle()
    {
        return rotAngle;
    }
}
