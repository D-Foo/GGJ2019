using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollisionScript : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.name)
        {
            case "BabyBird":
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("PICKUP BIRDY");
                }
                break;
        }
    }
}
