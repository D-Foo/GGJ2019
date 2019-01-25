using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMoveScript : MonoBehaviour {

    private float cylinderX;
    private float cylinderZ;
    static private float rotAngle;
    
	// Use this for initialization
	void Start () {
        cylinderX = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.x;
        cylinderX = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.z;
#if UNITY_EDITOR
     Debug.Log("XZ (" + cylinderX + ", " + cylinderZ + ")");
#endif
        rotAngle = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow))
        {
            rotAngle -= 30.0f * Time.deltaTime;
#if UNITY_EDITOR
            Debug.Log("LEFT");
#endif
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotAngle += 30.0f * Time.deltaTime;
#if UNITY_EDITOR
            Debug.Log("RIGHT");
#endif
        }

        gameObject.transform.position = new Vector3(5 * Mathf.Sin(Mathf.Deg2Rad * rotAngle), gameObject.transform.position.y, 5* -Mathf.Cos(Mathf.Deg2Rad * rotAngle));
#if UNITY_EDITOR
        Debug.Log("(" + Mathf.Sin(Mathf.Deg2Rad * rotAngle) + ", " + gameObject.transform.position.y + ", " + -Mathf.Cos(Mathf.Deg2Rad * rotAngle) +  ")");
#endif
    }

    static public float GetRotAngle()
    {
        return rotAngle;
    }
}
