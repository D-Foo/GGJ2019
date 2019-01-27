using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollisionScript : MonoBehaviour
{
    private GameObject mainCamera;
    private Vector3 viewAdjust;
    private AudioSource logHop;
    private AudioSource grassHop;
    BirdMoveScript bms;
    BirdCarryScript bcs;

    private void Start()
    {
        AudioSource[] audioSource = GetComponents<AudioSource>();
        bms = GetComponent<BirdMoveScript>();
        bcs = gameObject.GetComponent<BirdCarryScript>();
        grassHop = audioSource[1];
        logHop = audioSource[2];
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Branch":
                viewAdjust = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 6, gameObject.transform.position.z);
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, viewAdjust, 4.0f * Time.deltaTime);
                if (logHop.isPlaying != true && bms.IsMoving())
                    logHop.Play();
                break;

            case "Floor":
                viewAdjust = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, viewAdjust, 2.0f * Time.deltaTime);
                if (grassHop.isPlaying != true && bms.IsMoving())
                    grassHop.Play();
                break;

            default:
                break;
        }
    }
    private void OnTriggerStay(Collider trigger)
    {
        switch (trigger.gameObject.tag)
        {
            case "BirdHouse":
                Debug.Log("TRIGGER BIRDHOUSE");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (bcs.IsCarryingBaby())
                    {

                    }
                }
                break;

            default:
                break;
        }
    }
}
