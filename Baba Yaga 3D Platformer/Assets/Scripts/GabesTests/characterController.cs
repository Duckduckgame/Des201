using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public bool onGround = false;
    public bool doubleJumpOK = false;
    bool dashOK = true;

    public float jumpStrength;
    public float dashStrength;
    public float moveSpeed;
    public float turnSpeed;
    public float interpolation;

    Collider coll;
    Rigidbody rb;

    Transform camTransform;

    private float horiInput;
    private float vertInput;

    private float crntHoriM;
    private float crntVertM;

    Vector3 direction;
    float directionLength;

    Vector3 crntDirection;



    // Start is called before the first frame update
    void Start()
    {
        //Grab the components we need
        camTransform = Camera.main.transform;
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        #region flat Movement
        vertInput = Input.GetAxis("Vertical"); //Save our Inputs
        horiInput = Input.GetAxis("Horizontal");

        crntVertM = Mathf.Lerp(crntVertM, vertInput, Time.deltaTime * interpolation); //technically we're lerping twice unless we use GetAxisRaw, but this kinda feels good
        crntHoriM = Mathf.Lerp(crntHoriM, horiInput, Time.deltaTime * interpolation);


        direction = camTransform.forward * crntVertM + camTransform.right * crntHoriM; //set direction based on camera as well
        directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;
        Quaternion oldRot = transform.rotation;

        Debug.DrawRay(transform.position, transform.localPosition * 3, Color.green); //Current direction
        if (direction != Vector3.zero) {

            crntDirection = Vector3.Slerp(crntDirection, direction, Time.deltaTime * interpolation);
            crntDirection.y = 0;
            transform.position += crntDirection * moveSpeed * Time.deltaTime; //the actual moving
            transform.rotation = Quaternion.Lerp(oldRot, Quaternion.LookRotation(direction), Time.deltaTime * turnSpeed); //Rotating character towards direction of travel. Lerp stops it snapping
           
        }

        #endregion
        


        #region Jumps
        if (Input.GetButtonDown("Jump")){

            //double jump
            if (onGround == false && doubleJumpOK == true)
            {
                Vector3 crntVelo = rb.velocity;
                crntVelo.y = 0;
                rb.velocity = Vector3.zero; //Important to zero out the velocity first. Addforce will otherwise be deducted from the downwards velocity
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
                doubleJumpOK = false;
            }
            //jump
            if (onGround == true) {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
                doubleJumpOK = true;
            }
            
        }
        if (Input.GetButtonDown("Fire1")) {
            if (onGround == false && dashOK == true)
            {
                rb.AddRelativeForce(new Vector3(0, 0.25f, 1) * dashStrength, ForceMode.VelocityChange);
                dashOK = false;
            }
        }
    }
    #endregion

    public void ChildCollisionEnter(GameObject child) {
        if (child.tag == "PlayerFloorCollision")
        {
            onGround = true;
            doubleJumpOK = false;
            dashOK = false;
        }
    }
    public void ChildCollisionStay(GameObject child) {
        if (child.tag == "PlayerFloorCollision")
        {
            onGround = true;
            doubleJumpOK = false;
            dashOK = false;
        }
    }

        public void ChildCollisionLeave(GameObject child) {
            if (child.tag == "PlayerFloorCollision")
            {
                onGround = false;
                doubleJumpOK = true;
                dashOK = true;
            }
        }
    }


