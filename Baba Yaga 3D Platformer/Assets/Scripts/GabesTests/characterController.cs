using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class characterController : MonoBehaviour
{
    public int lostSoulsCount = 0;
    public int scrollCount = 0;
    public bool onGround = false;
    public bool doubleJumpOK = false;
    bool dashOK = true;

    public float jumpStrength = 6f;
    public float dashStrength = 7f;
    public float moveSpeed = 5f;
    public float turnSpeed = 6f;
    public float interpolation = 20f;

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


    //Player sounds
    AudioSource dashSound;
    AudioSource JumpStart;
    AudioSource JumpEnd;
    AudioSource[] audios;


    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        //Grab the components we need
        camTransform = Camera.main.transform;
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        audios = new AudioSource[3];
        dashSound = audios[0];
        JumpEnd = audios[1];
        JumpStart = audios[2];

        anim = GetComponent<Animator>();

       
       

    }

    // Update is called once per frame
    void Update()
    {
        #region flat Movement
        vertInput = Input.GetAxis("Vertical"); //Save our Inputs
        horiInput = Input.GetAxis("Horizontal");


        #region animation
        if (vertInput != 0 || horiInput != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else if (vertInput == 0 || horiInput == 0) {
            anim.SetBool("isMoving", false);
        }
        if (anim.GetBool("isDoubleJumping") == true) {
            anim.SetBool("isDoubleJumping", false);
        }
        if (anim.GetBool("isDashing") == true)
        {
            anim.SetBool("isDashing", false);
        }
        #endregion


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
                //dashSound.Play();
                Vector3 crntVelo = rb.velocity;
                crntVelo.y = 0;
                rb.velocity = Vector3.zero; //Important to zero out the velocity first. Addforce will otherwise be deducted from the downwards velocity
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
                doubleJumpOK = false;
                anim.SetBool("isJumping", false);
                anim.SetBool("isDashing", false);
                anim.SetBool("isDoubleJumping", true);
            }
            //jump
            if (onGround == true) {
                //JumpStart.Play();              
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
                doubleJumpOK = true;
                anim.SetBool("isJumping", true);
               
            }
            
        }
        if (Input.GetButtonDown("Fire1")) {
            if (onGround == false && dashOK == true)
            {
                //dashSound.Play();
                rb.AddRelativeForce(new Vector3(0, 0.25f, 1) * dashStrength, ForceMode.VelocityChange);
                dashOK = false;
                anim.SetBool("isDoubleJumping", false);
                anim.SetBool("isJumping", false);
                anim.SetBool("isDashing", true);
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
            anim.SetBool("isLanding", true);
            anim.SetBool("isJumping", false);
            anim.SetBool("isDoubleJumping", false);
            
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
                anim.SetBool("isLanding", false);
            }
        }
    }


