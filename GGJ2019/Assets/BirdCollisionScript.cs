using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollisionScript : MonoBehaviour
{
    private GameObject mainCamera;
    private Vector3 viewAdjust;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Branch":
                viewAdjust = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 4, gameObject.transform.position.z);
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, viewAdjust, 4.0f * Time.deltaTime);
                break;

            default:
                viewAdjust = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, viewAdjust, 2.0f * Time.deltaTime);
                break;
        }
    }
}
