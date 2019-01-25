using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMoveScript : MonoBehaviour {

    private float cylinderX;
    private float cylinderZ;
    static private float rotAngle;
    private bool isFalling;
    
	// Use this for initialization
	void Start () {
        cylinderX = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.x;
        cylinderZ = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.z;
#if UNITY_EDITOR
     Debug.Log("XZ (" + cylinderX + ", " + cylinderZ + ")");
#endif
        rotAngle = 0.0f;
        isFalling = false;
    }
	
	// Update is called once per frame
	void Update () {
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

        gameObject.transform.position = new Vector3(5 * Mathf.Sin(Mathf.Deg2Rad * rotAngle), gameObject.transform.position.y, 5 * -Mathf.Cos(Mathf.Deg2Rad * rotAngle));

        if (rotAngle < 0)
        {
            rotAngle += 360;
        }
        else if(rotAngle > 360)
        {
            rotAngle -= 360;
        }

        Debug.Log("rotAngle: " + rotAngle);

        if(Input.GetKeyDown(KeyCode.Space) && !isFalling)
        {
            isFalling = true;
            Rigidbody birdBody = gameObject.GetComponent<Rigidbody>();
            birdBody.velocity = new Vector3(birdBody.velocity.x, 20, birdBody.velocity.z);
        }


#if UNITY_EDITOR
        //Debug.Log("(" + Mathf.Sin(Mathf.Deg2Rad * rotAngle) + ", " + gameObject.transform.position.y + ", " + -Mathf.Cos(Mathf.Deg2Rad * rotAngle) +  ")");
#endif
    }

    public void OnCollisionEnter(Collision collision)
    {
        isFalling = false;
    }

    static public float GetRotAngle()
    {
        return rotAngle;
    }
}
