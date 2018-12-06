using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMenuManager : MonoBehaviour {

    [SerializeField]
    GameObject UI;
    [SerializeField]
    ThirdPersonCameraController playerCameraController;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ShowUI();
    }

    public void ShowUI()
    {
        if (Input.GetKey(KeyCode.A))
        {
            UI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerCameraController.movingCamera = false;
        }
        else
        {
            UI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerCameraController.movingCamera = true;
        }
    }

}

/*
    
*/