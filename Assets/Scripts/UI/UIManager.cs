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

    [SerializeField]
    private Material constructingMaterial;
    [SerializeField]
    private Material constructingErrorMaterial;

	// Use this for initialization
	void Start () {	
        constructionPanel.SetActive(false);
        bookPanel.SetActive(false);
	}

    private void setMaterialAvailable()
    {
        setConstructingMaterial(constructingMaterial);
    }

    private void setMaterialError()
    {
        setConstructingMaterial(constructingErrorMaterial);
    }


    private void cancelBuild()
    {
        if (isBuilding)
        {
            isBuilding = false;
            Destroy(instanciatedBuilding.gameObject);
        }
    }


    private void setConstructingMaterial(Material material)
    {

        if (instanciatedBuilding == null)
        {
            return;
        }

        Renderer rend = instanciatedBuilding.transform.GetChild(0).GetComponent<Renderer>();
        if (rend == null)
        {
            rend = instanciatedBuilding.transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
            if (rend == null)
                return;
        }
        rend.material = material;
    }



	// Update is called once per frame
	void Update () {
        if(panelEnabled && Input.GetKeyUp(KeyCode.Tab)){
            panelEnabled = false;
            constructionPanel.SetActive(false);
            Cursor.visible = false;
            playerCameraController.movingCamera = true;
        }

        if(!panelEnabled && Input.GetKeyDown(KeyCode.Tab)){
            constructionPanel.SetActive(true);
            panelEnabled = true;
            Cursor.visible = true;
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
                //Debug.Log("Angle : " + Vector3.Angle(instanciatedBuilding.transform.position, playerCameraController.transform.position));
            }

            if(Input.GetMouseButtonDown(0)){
                isBuilding = false;
                Renderer rend = instanciatedBuilding.GetComponent<Renderer>();
                if (rend == null)
                {
                    rend = instanciatedBuilding.GetComponentInChildren<Renderer>();
                    if (rend == null)
                        return;
                }
                rend.material = defaultMaterial;
                instanciatedBuilding.GetComponentInChildren<Collider>().enabled = true;
                instanciatedBuilding.GetComponent<Building>().build();
                Debug.Log("Build !");
            }
        }

        if(!panelEnabled && Input.GetKeyDown(KeyCode.J)){
            bookPanel.SetActive(!bookPanel.activeSelf);
            Cursor.visible = bookPanel.activeSelf;
            playerCameraController.movingCamera = !bookPanel.activeSelf;
        }

	}

    public void constructBuilding(GameObject building){
        isBuilding = true;
        instanciatedBuilding = Instantiate(building);
        Renderer rend = instanciatedBuilding.GetComponent<Renderer>();
        if(instanciatedBuilding.GetComponentInChildren<Collider>() != null)
            instanciatedBuilding.GetComponentInChildren<Collider>().enabled = false;
        if (rend == null)
        {
            rend = instanciatedBuilding.GetComponentInChildren<Renderer>();
            if (rend == null)
            {
                Debug.Log("Material not found on : " + instanciatedBuilding.name);
                return;
            }
        }
        defaultMaterial = rend.material;
        setMaterialAvailable();
    } 
}
