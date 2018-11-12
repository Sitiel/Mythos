using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour {

    [Header("Player to follow")]
    [SerializeField]
    Transform lookAt;

    [SerializeField]
    private float distance = 10.0f;

    [SerializeField]
    private float minYAngle;
    [SerializeField]
    private float maxYAngle;
    [Header("Layer(s) to include for camera collision")]
    public LayerMask CamOcclusion;

    private float currentX = 0, currentY = 0;
    private float sensivityX = 4.0f, sensivityY = 4.0f;
    public bool movingCamera = true;


    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        if (movingCamera)
        {
            currentX += Input.GetAxis("Mouse X") * sensivityX;
            currentY = Mathf.Clamp(currentY - Input.GetAxis("Mouse Y") * sensivityY, minYAngle, maxYAngle);
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), 5, 25);
        }
    }


    private void collisionReplacementForCamera(){
        RaycastHit wallHit = new RaycastHit();

        //We trace a ray between the camera and the player and we put the camera at the collision point if there is something
        if (Physics.Linecast(lookAt.position, transform.position, out wallHit, CamOcclusion))
        {
            transform.position = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, wallHit.point.y + wallHit.normal.y * 0.5f, wallHit.point.z + wallHit.normal.z * 0.5f);
        }
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = lookAt.position + rotation * dir;
        collisionReplacementForCamera();

        transform.LookAt(new Vector3(lookAt.position.x, (lookAt.position.y + 2f), lookAt.position.z));

    }
}
