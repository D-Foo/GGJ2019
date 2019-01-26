using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour {

    private float cylinderX;
    private float cylinderZ;

    // Use this for initialization
    void Start () {
        cylinderX = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.x;
        cylinderZ = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        //float playerRot = GameObject.FindGameObjectWithTag("Player").GetComponent("rotAngle");
        float playerRot = BirdMoveScript.GetRotAngle();
       // Debug.Log("playerRot: " + playerRot);
        //Camera thisCam = gameObject.GetComponent<Camera>();
        //gameObject.transform.rotation = Quaternion.Euler(-playerRot, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
  
        gameObject.transform.position = new Vector3(10 * Mathf.Sin(Mathf.Deg2Rad * playerRot), GameObject.FindGameObjectWithTag("Player").transform.position.y + 2.0f, 10 * -Mathf.Cos(Mathf.Deg2Rad * playerRot));
        gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform, new Vector3(0, 1, 0));
    }
}
