using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour {

    BirdMoveScript playerMoveScript;
    // Use this for initialization
    void Start ()
    {
        playerMoveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BirdMoveScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //float playerRot = GameObject.FindGameObjectWithTag("Player").GetComponent("rotAngle");
        float playerRot = BirdMoveScript.GetRotAngle();
        float playerRadius = playerMoveScript.GetRadius();
        float camRadius = 7.0f;
       // Debug.Log("playerRot: " + playerRot);
        //Camera thisCam = gameObject.GetComponent<Camera>();
        //gameObject.transform.rotation = Quaternion.Euler(-playerRot, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
        
        gameObject.transform.position = new Vector3((camRadius + playerRadius) * Mathf.Sin(Mathf.Deg2Rad * playerRot), gameObject.transform.position.y, (camRadius + playerRadius) * -Mathf.Cos(Mathf.Deg2Rad * playerRot));
        gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform, new Vector3(0, 1, 0));
    }
}
