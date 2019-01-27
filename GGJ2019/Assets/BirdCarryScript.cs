using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCarryScript : MonoBehaviour {

    static private bool mouthCarrying;
    static private bool isCarryingBaby;
    static GameObject mouthCarriedObject;
    private bool clawCarrying;
    GameObject clawCarriedObject;

    public float localX = -1.07f;
    public float localY = -0.22f;
    public float localZ = -0.11f;

	// Use this for initialization
	void Start ()
    {
        clawCarrying = false;
        clawCarriedObject = null;
        mouthCarrying = false;
        mouthCarriedObject = null;
        isCarryingBaby = false;
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
    public bool IsCarryingBaby()
    {
        return clawCarrying;
    }
    static public void setIsCarryingBaby(bool input)
    {
        isCarryingBaby = input;
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
    public GameObject GetCarriedBird()
    {
        if(isCarryingBaby)
        {
            return clawCarriedObject;
        }
        else
        {
            return null;
        }
    }


    private bool AttemptToCarry()
    {
        float pickUpRange = 3.5f;
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
            //Attach to parent and apply local position transform
            nearestBaby.transform.SetParent(GameObject.FindGameObjectWithTag("Bird Head").transform);
            nearestBaby.transform.localPosition = new Vector3(-1.75f, 0.0f, 0.0f);
            nearestBaby.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 75.0f);
            clawCarriedObject = nearestBaby;
         
            return true;

        }

        
        return false;
    }

    
}
