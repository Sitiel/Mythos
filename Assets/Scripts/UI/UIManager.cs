using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField]
    GameObject constructionPanel;
    [SerializeField]
    GameObject bookPanel;
    [SerializeField]
    ThirdPersonCameraController playerCameraController;

    private bool panelEnabled = true;
    private bool isBuilding = false;
    private GameObject instanciatedBuilding;
    private Material defaultMaterial;
    public LayerMask camOcclusion;
    Build currentBuild;


	// Use this for initialization
	void Start () {	
        panel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bookPanel.SetActive(false);
	}

    private void cancelBuild()
    {
        if (isBuilding)
        {
            isBuilding = false;
            Destroy(instanciatedBuilding.gameObject);
        }
    }



	// Update is called once per frame
	void Update () {
        if(panelEnabled && Input.GetKeyUp(KeyCode.Tab)){
            panelEnabled = false;
            constructionPanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerCameraController.movingCamera = true;
        }

        if(!panelEnabled && Input.GetKeyDown(KeyCode.Tab)){
            constructionPanel.SetActive(true);
            panelEnabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerCameraController.movingCamera = false;
        }

        if (isBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,10000, camOcclusion))
            {
                instanciatedBuilding.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                instanciatedBuilding.transform.rotation = Quaternion.Euler(instanciatedBuilding.transform.eulerAngles.x, playerCameraController.transform.eulerAngles.y, instanciatedBuilding.transform.eulerAngles.z);
           }

            if(Input.GetMouseButtonDown(0)){
                Vector3 lastPos = instanciatedBuilding.transform.position;
                Quaternion lastRot = instanciatedBuilding.transform.rotation;
                Destroy(instanciatedBuilding);
                instanciatedBuilding = Instantiate(currentBuild.building, lastPos, lastRot);
                isBuilding = false;

                instanciatedBuilding.GetComponentInChildren<Building>().build();
                Debug.Log("Build !");
            }
        }

        if(!panelEnabled && Input.GetKeyDown(KeyCode.J)){
            bookPanel.SetActive(!bookPanel.activeSelf);
            Cursor.visible = bookPanel.activeSelf;
            playerCameraController.movingCamera = !bookPanel.activeSelf;
        }

	}

    public void constructBuilding(Build build){
        currentBuild = build;
        isBuilding = true;
        instanciatedBuilding = Instantiate(currentBuild.preview_ok);
    } 
}
