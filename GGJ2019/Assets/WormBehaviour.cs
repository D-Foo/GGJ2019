using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBehaviour : MonoBehaviour {

    private bool isCarried;

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
}
