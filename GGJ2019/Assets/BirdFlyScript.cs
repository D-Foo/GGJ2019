using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlyScript : MonoBehaviour {

    BirdCarryScript bcs;
    BirdMoveScript bms;
    float flightMeter;
    Animator animator;
    AudioSource wingFlap;


	// Use this for initialization
	void Start () {
        bcs = gameObject.GetComponent<BirdCarryScript>();
        bms = gameObject.GetComponent<BirdMoveScript>();
        wingFlap = gameObject.GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
        flightMeter = 1.35f;
	}
	
	// Update is called once per frame 
	void Update ()
    {
        animator.speed = 1.0f;  //Reset animation speed on update
		if(Input.GetKey(KeyCode.Space))
        {
            if (!bms.IsFalling() && flightMeter > 0 && bms.IsAirborne())
            {
                flightMeter -= Time.deltaTime;
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 550.0f * Time.deltaTime, 0.0f));
                animator.speed = 3.5f;  //Increase animation speed while flying
                if (wingFlap.isPlaying != true && !bms.IsFalling())
                    wingFlap.Play();
                Debug.Log("FLIGHT" + flightMeter.ToString());
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Branch")
        {
            flightMeter = 1.35f;
            bms.HasLanded();
        }
    }
}
