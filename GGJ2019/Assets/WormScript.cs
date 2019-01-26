using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormScript : MonoBehaviour {

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
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position; //TODO ADD CLAW OFFSET
        }
    }

    public void SetCarried(bool carried)
    {
        isCarried = carried;
    }
}
