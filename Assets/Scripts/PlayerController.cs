using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;
    [SerializeField]
    private float jumpForce;


    private CapsuleCollider capsuleCollider;

    private bool isWalk = false;
    private bool isRun = false;
    private bool isGround = true;

    //check if player is walking
    private Vector3 lastPos;
    
    

    private Rigidbody myRigid;
    private GunController theGunController;
    private Crosshair theCrosshair;
    private StatusController theStatusController;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        theGunController = FindObjectOfType<GunController>();
        theCrosshair = FindObjectOfType<Crosshair>();
        theStatusController = FindObjectOfType<StatusController>();

        applySpeed = walkSpeed;



    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        Move();
        MoveCheck();
        
    }
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && theStatusController.GetCurrentSP() > 0)
        {
            Jump();
        }
    }
    private void Jump()
        {
            theStatusController.DecreaseStamina(10);
            myRigid.velocity = transform.up * jumpForce;
        }
    private void IsGround()
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        theCrosshair.JumpingAnimation(!isGround);
        }

    
    private void TryRun() 
    {
        if(Input.GetKey(KeyCode.LeftShift) && theStatusController.GetCurrentSP() > 0)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || theStatusController.GetCurrentSP() <= 0)
        {
            RunningCancel();
        }
    }
    private void Running()
    {   
        isRun = true;
        applySpeed = runSpeed;
        theGunController.CancelFineSight();
        theCrosshair.RunningAnimation(isRun);
        theStatusController.DecreaseStamina(5);


    }

    private void RunningCancel()
    {
        isRun = false;
        theCrosshair.RunningAnimation(isRun);
        applySpeed = walkSpeed;
    }


    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);


    }
    private void MoveCheck()
    {
        if (!isRun)
        {
            if(Vector3.Distance(lastPos, transform.position) >= 0.01f)
            {
                isWalk = true;
            }
            else
            {
                isWalk = false;
            }
            theCrosshair.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }
     
     
    }
}
