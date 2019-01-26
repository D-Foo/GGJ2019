using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdColliding : MonoBehaviour {

    private bool isCarried;

    // Use this for initialization
    void Start()
    {
        isCarried = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCarried)
        {
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position; //TODO ADD MOUTH OFFSET
        }
    }

    public void SetCarried(bool carried)
    {
        isCarried = carried;
    }

    
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collide");
        switch (collider.gameObject.tag)
        {
            case "Worm":
                Debug.Log("WORM");
                break;

            case "Baby Bird":
                Debug.Log("Feed Worm");
                break;

            case "Player":
                Debug.Log("Player Hit Worm");
                break;
        }
    }
}
