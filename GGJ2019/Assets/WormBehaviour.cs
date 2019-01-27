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
            //transform.position = GameObject.FindGameObjectWithTag("Player").transform.position; //TODO ADD MOUTH OFFSET
        }
    }

    public void SetCarried(bool carried)
    {
        Debug.Log("Start Carrying");
        isCarried = carried;
        transform.SetParent(GameObject.FindGameObjectWithTag("Bird Head").transform);
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        transform.localPosition = new Vector3(0.2f, 1.0f, 0.0f);
       // GameObject.tag("Bird Head")
       // transform.localPosition = new Vector3(3.0f, 3.3f, 0);
    }
}
