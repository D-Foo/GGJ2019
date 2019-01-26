using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCarryScript : MonoBehaviour {

    static private bool mouthCarrying;
    static GameObject mouthCarriedObject;
    private bool clawCarrying;
    GameObject clawCarriedObject;

	// Use this for initialization
	void Start ()
    {
        clawCarrying = false;
        clawCarriedObject = null;
        mouthCarrying = false;
        mouthCarriedObject = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.E))
        {
            if (!clawCarrying)
            {
                //ATTEMPT TO PICK UP CARRIED ITEM HERE
                bool startCarry = AttemptToCarry();
                if (startCarry)
                {
                    clawCarrying = true;
                    Debug.Log("Started Carrying");
                }
                else
                {
                    //TRIGGER CARRY NOT STARTED FEEDBACK
                }
            }
                
            else if (clawCarrying)
            {
                //DROP CARRIED ITEM
                clawCarrying = false;
                clawCarriedObject.SendMessage("SetCarried", false);
                clawCarriedObject = null;
                Debug.Log("Drop");
            }
            
        }
	}

    static public bool IsMouthCarrying()
    {
        return mouthCarrying;
    }
    static public void StartMouthCarry(GameObject worm)
    {
        mouthCarrying = true;
        mouthCarriedObject = worm;
    }
    static public void EndMouthCarry()
    {
        mouthCarrying = false;
        Destroy(mouthCarriedObject);
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
            clawCarriedObject = nearestBaby;
            nearestBaby.SendMessage("SetCarried", true);
            return true;

        }

        
        return false;
    }
}
