using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMoveScript : MonoBehaviour {

    private float cylinderX;
    private float cylinderZ;
    static private float rotAngle;
    static private float radius;
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
        radius = 5.0f;
        hasJumped = false;
        isFalling = false;
    }
	
	// Update is called once per frame
	void Update () {

        //Arrow Key Controls


		if(Input.GetKey(KeyCode.LeftArrow))
        {
            rotAngle -= 30.0f * Time.deltaTime;
#if UNITY_EDITOR
           // Debug.Log("LEFT");
#endif
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotAngle += 30.0f * Time.deltaTime;
#if UNITY_EDITOR
           // Debug.Log("RIGHT");
#endif
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            radius -= 2.0f * Time.deltaTime;
#if UNITY_EDITOR
            // Debug.Log("IN");
#endif
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            radius += 2.0f * Time.deltaTime;
#if UNITY_EDITOR
            // Debug.Log("OUT");
#endif
        }







        //Clamp rotation angle and radius

        if(radius < 1.0f)
        {
            radius = 1.0f;
        }
        else if(radius > 5.0f)
        {
            radius = 5.0f;
        }

        if (rotAngle < 0)
        {
            rotAngle += 360;
        }
        else if(rotAngle > 360)
        {
            rotAngle -= 360;
        }


        //Update position
        gameObject.transform.position = new Vector3(radius * Mathf.Sin(Mathf.Deg2Rad * rotAngle), gameObject.transform.position.y, radius * -Mathf.Cos(Mathf.Deg2Rad * rotAngle));

        // Debug.Log("rotAngle: " + rotAngle);

        //Check for isFalling
        if (hasJumped && birdBody.velocity.y < 0 && !isFalling)
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
