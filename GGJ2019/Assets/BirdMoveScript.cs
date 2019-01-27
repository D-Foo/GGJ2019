using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMoveScript : MonoBehaviour {


    private enum CardinalMoveDir { north, east, south, west };





    private float cylinderX;
    private float cylinderZ;
    static private float rotAngle;
    static private float radius;
    private bool isAirborne;
    private bool isFalling;
    Rigidbody birdBody;
    Animator animator;
    private bool firstMove; //Used for checking whether to play slow first hop or not
    private float moveLockTimer;
    private float firstHopMoveLockTime;
    private float takeOffMoveLockTime;
    private bool moveLocked;
    private float flyCountdown;
    private bool takeOffPrep;
    private CardinalMoveDir playerMoveDir;
    float spinAngle; //For rotating the bird to face left/right
    float spinSpeed;

	// Use this for initialization
	void Start () {
        cylinderX = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.x;
        cylinderZ = GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.z;
        birdBody = gameObject.GetComponent<Rigidbody>();
#if UNITY_EDITOR
      //  Debug.Log("XZ (" + cylinderX + ", " + cylinderZ + ")");
#endif
        rotAngle = 0.0f;
        radius = 15.0f;
       
        isAirborne = false;
        isFalling = false;

        //Fix for animation not playing
        animator = gameObject.GetComponent<Animator>();
        moveLockTimer = 0.0f;
        firstHopMoveLockTime = 0.45f;
        takeOffMoveLockTime = 0.4f;
        flyCountdown = takeOffMoveLockTime;
        takeOffPrep = false;
        moveLocked = false;
        firstMove = true;
        playerMoveDir = CardinalMoveDir.east;
        spinAngle = 0.0f;
        spinSpeed = 300.0f;
    }
	
	// Update is called once per frame
	void Update () {

        //Arrow Key Controls

        if(!moveLocked)
        {
            if (Input.GetKey(KeyCode.W))
            {
                radius -= 5.0f * Time.deltaTime;
                playerMoveDir = CardinalMoveDir.north;
            }

            if (Input.GetKey(KeyCode.A))
            {
                rotAngle -= 22.5f * Time.deltaTime;
                playerMoveDir = CardinalMoveDir.west;
            }

            if (Input.GetKey(KeyCode.S))
            {
                radius += 5.0f * Time.deltaTime;
                playerMoveDir = CardinalMoveDir.south;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rotAngle += 22.5f * Time.deltaTime;
                playerMoveDir = CardinalMoveDir.east;
            }
        }
        else
        {
            moveLockTimer -= Time.deltaTime;
            if(moveLockTimer <= 0)
            {
                moveLocked = false;
                moveLockTimer = 0.0f;

                //If was move locked due to taking off
                if (takeOffPrep)
                {
                    isAirborne = true;
                    birdBody.velocity = new Vector3(birdBody.velocity.x, 8.0f, birdBody.velocity.z);
                    takeOffPrep = false;
                }
            }
        }

        //Start playing hop anim and lock player in place for a short amount of time
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!isAirborne && !takeOffPrep)
            {
                if (firstMove)
                {
                    firstMove = false;
                    Debug.Log("First Move");
                    moveLocked = true;
                    moveLockTimer = firstHopMoveLockTime;
                    //TODO: LOCK PLAYER
                    animator.Play("Hop");
                }
            }
        }

        //If at any point neither of the WASD keys are held down and not taking off or flying reset first hop
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            if (!takeOffPrep && !isAirborne)
            {
                firstMove = true;
                animator.Play("Idle");
            }
        }



        //Update spin angle
        int targetAngle = 0;
        switch (playerMoveDir)
        {
            case CardinalMoveDir.north:
                //Logic for fast spins
                if(spinAngle < 90 && spinAngle >= 0)
                {
                    spinAngle += 360;
                }
                targetAngle = 270;
                break;
            case CardinalMoveDir.east:
                if (spinAngle >= 360)
                {
                    spinAngle -= 360;
                }
                //Logic for fast spins
                if (spinAngle >= 270 && spinAngle < 360)
                {
                    spinAngle -= 360;
                }
                targetAngle = 0;
                break;
            case CardinalMoveDir.west:
                if (spinAngle >= 360)
                {
                    spinAngle -= 360;
                }
                targetAngle = 180;
                break;
            case CardinalMoveDir.south:
                if (spinAngle >= 360)
                {
                    spinAngle -= 360;
                }
                targetAngle = 90;
                break;
        }
        UpdateSpinAngle(targetAngle);



        //Clamp rotation angle and radius
        float minRadius = 7.25f;
        if(radius < minRadius)
        {
            radius = minRadius;
        }
        else if(radius > 35.0f)
        {
            radius = 35.0f;
        }

        if (rotAngle < 0)
        {
            rotAngle += 360;
        }
        else if(rotAngle > 360)
        {
            rotAngle -= 360;
        }


        //Update position
        gameObject.transform.position = new Vector3(radius * Mathf.Sin(Mathf.Deg2Rad * rotAngle), gameObject.transform.position.y, radius * -Mathf.Cos(Mathf.Deg2Rad * rotAngle));

        



        //Check for isFalling
        if (isAirborne && birdBody.velocity.y < 0 && !isFalling)
        {
            //Debug.Log("Fall trigger");
            isFalling = true;
        }

        //Press Space to Jump
        if(Input.GetKeyDown(KeyCode.Space) && !isAirborne && !takeOffPrep)
        {
            Debug.Log("Start Jump");
            takeOffPrep = true;
            animator.Play("TakeOff");
            moveLocked = true;
            moveLockTimer = takeOffMoveLockTime;
        }

        //Hold space while falling to slow the fall
        if(isFalling && Input.GetKey(KeyCode.Space))
        {
            birdBody.velocity += new Vector3(0.0f, 7.0f * Time.deltaTime, 0.0f);
          //  Debug.Log("Fall mitigation, velocity: " + birdBody.velocity.y);
        }

        gameObject.transform.LookAt(new Vector3(GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.x, this.transform.position.y, GameObject.FindGameObjectWithTag("Center Cylinder").transform.position.z));

        //gameObject.transform.rotation = Quaternion.Euler(0, 90 - rotAngle, -90);
        gameObject.transform.rotation = Quaternion.Euler(0, -rotAngle + spinAngle, 0);
      //  Debug.Log("Rotation: (" + transform.rotation.ToString());
#if UNITY_EDITOR
        //Debug.Log("(" + Mathf.Sin(Mathf.Deg2Rad * rotAngle) + ", " + gameObject.transform.position.y + ", " + -Mathf.Cos(Mathf.Deg2Rad * rotAngle) +  ")");
#endif
    }

    //TODO: Maybe Delete This
    //public void OnCollisionEnter(Collision collision)
    //{
    //    isAirborne = false;
    //    isFalling = false;
    //}

   private void UpdateSpinAngle(int targetAngle)
    {
        if (spinAngle != targetAngle)
        {
            if (spinAngle > targetAngle)
            {
                //Update
                spinAngle -= spinSpeed * Time.deltaTime;
                //Clamp
                if (spinAngle < targetAngle)
                {
                    spinAngle = targetAngle;
                }
            }
            else
            {
                if (spinAngle < targetAngle)
                {
                    //Update
                    spinAngle += spinSpeed * Time.deltaTime;
                    //Clamp
                    if (spinAngle > targetAngle)
                    {
                        spinAngle = targetAngle;
                    }
                }
            }
        }
    }


    //Getters + Setters
    static public float GetRotAngle()
    {
        return rotAngle;
    }

    public float GetRadius()
    {
        return radius;
    }

    public bool IsFalling()
    {
        return isFalling;
    }

    public bool IsAirborne()
    {
        return isAirborne;
    }

    public void HasLanded()
    {
        isAirborne = false;
        isFalling = false;
        if(!firstMove)
        {
            animator.Play("FastHop");
        }
    }
}

