using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlyScript : MonoBehaviour {

    BirdCarryScript bcs;
    BirdMoveScript bms;
    float flightMeter;


	// Use this for initialization
	void Start () {
        bcs = gameObject.GetComponent<BirdCarryScript>();
        bms = gameObject.GetComponent<BirdMoveScript>();
        flightMeter = 1.35f;
	}
	
	// Update is called once per frame 
	void Update ()
    {
		if(Input.GetKey(KeyCode.Space))
        {
            if (!bms.IsFalling() && flightMeter > 0)
            {
                flightMeter -= Time.deltaTime;
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 550.0f * Time.deltaTime, 0.0f));
                Debug.Log("FLIGHT" + flightMeter.ToString());
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Floor")
        {
            flightMeter = 0.8f;
        }
    }
}
