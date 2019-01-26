using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCarryScript : MonoBehaviour {


    private bool carrying;

	// Use this for initialization
	void Start ()
    {
        carrying = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.E))
        {
            if(carrying)
            {
                //DROP CARRIED ITEM
                carrying = false;
                Debug.Log("Drop");
            }
            if(!carrying)
            {
                //ATTEMPT TO PICK UP CARRIED ITEM HERE
                bool startCarry = AttemptToCarry();
                if(startCarry)
                {
                    carrying = true;
                    Debug.Log("Started Carrying"); 
                }
                else
                {
                    //TRIGGER CARRY NOT STARTED FEEDBACK
                }
            }
        }
	}

    private bool AttemptToCarry()
    {
        float pickUpRange = 1.0f;
        GameObject[] babies;
        babies = GameObject.FindGameObjectsWithTag("Baby Bird");
        GameObject nearestBaby = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        //Get nearest baby bird
        foreach (GameObject baby in babies)
        {
            Vector3 diff = baby.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                nearestBaby = baby;
                distance = curDistance;
            }
        }
        
        if(distance < pickUpRange)
        {
            //TODO: ALSO RETURN CLOSEST BABY
            Debug.Log("Pick up");
            return true;

        }

        
        return false;
    }
}
