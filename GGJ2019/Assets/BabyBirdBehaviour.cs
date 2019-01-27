using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBirdBehaviour : MonoBehaviour {

    private bool isCarried;
    private bool isFed;

	// Use this for initialization
	void Start () {
        isCarried = false;
        isFed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isCarried)
        {
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position; //TODO ADD CLAW OFFSET
        }
	}

    //TODO: Fix this logic
    public void SetCarried(bool carried)
    {
        if (isFed)
        {
            isCarried = carried;
        }
    }

    public void GetFed()
    {
        isFed = true;
        GetComponent<Animator>().Play("Idle");
    }
}
