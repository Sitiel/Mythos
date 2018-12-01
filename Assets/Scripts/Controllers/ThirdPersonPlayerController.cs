    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayerController : MonoBehaviour
{

    private CharacterController characterController;
    [SerializeField]
    private ThirdPersonCameraController cameraController;

    private Animator playerAnimator;

    [SerializeField]
    private float speed = 5;

    private float mass = 3.0f;
    float impact = 0;

    private float gravity = 3f;
    bool jumping = false;
    float vSpeed = 0;

    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    void jump(float force)
    {
        if (!jumping)
        {
            impact += force / mass;
            jumping = true;
            //playerAnimator.SetInteger("Jumping", 1);
        }
    }

    private void calculateVSpeed(){
        
        vSpeed += gravity * Time.deltaTime;

        //We do not reduce the impact force using vSpeed to create a Quadradic like jump

        if (impact > 0.2) vSpeed = -impact * Time.deltaTime;

        //Lerp is used in the jump to decrease the impact slowly to 0

        impact = Mathf.Lerp(impact, 0, 5 * Time.deltaTime);

        //If we touch the ground, we are not falling anymore so our vSpeed is 0 (if vSpeed is negative we are jumping)
        if (characterController.isGrounded && vSpeed >= 0)
        {
            vSpeed = 0;
        }
    }


    private void calculateCurrentState(){
        if (characterController.isGrounded && jumping && impact < 0.2)
        {
            jumping = false;
            //playerAnimator.SetInteger("Jumping", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        int reversed = 1, reverseAngle = 0;

        float direction = 0;

        if (Input.GetKey("z"))
        {
            transform.rotation = cameraController.transform.rotation;
            direction = speed;
        }

        if (Input.GetKey("s"))
        {
            reverseAngle = 180;
            reversed = -1;
            direction = speed;
            transform.rotation = Quaternion.Euler(new Vector3(0f, cameraController.transform.rotation.eulerAngles.y + 180, 0f));
        }

        /*if (direction > 0)
        {
            playerAnimator.SetFloat("Speed", 2);
        }
        else
        {
            playerAnimator.SetFloat("Speed", 0);
        }*/

        transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));

        if (Input.GetKey("q"))
        {
            if(direction == 0){
                transform.rotation = Quaternion.Euler(new Vector3(0f, cameraController.transform.rotation.eulerAngles.y + 90, 0f));
                direction = -speed;
            }
            else{
                transform.rotation = Quaternion.Euler(new Vector3(0f, cameraController.transform.rotation.eulerAngles.y + reverseAngle - (45 * reversed), 0f));
            }

        }

        if (Input.GetKey("d"))
        {
            if (direction == 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, cameraController.transform.rotation.eulerAngles.y + 90, 0f));
                direction = speed;
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, cameraController.transform.rotation.eulerAngles.y + reverseAngle + (45 * reversed), 0f));
            }

        }

        if (Input.GetKey("space"))
        {
            jump(100);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * direction * Time.deltaTime;

        calculateCurrentState();
        calculateVSpeed();

        forward.y = -vSpeed;
        characterController.Move(forward);
    }
}
