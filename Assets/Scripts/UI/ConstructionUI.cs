using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionUI : MonoBehaviour {

    [SerializeField]
    GameObject panel;
    [SerializeField]
    ThirdPersonCameraController playerCameraController;

    private bool panelEnabled = true;
    private bool isBuilding = false;
    private GameObject instanciatedBuilding;
    private Material defaultMaterial;

    [SerializeField]
    private Material constructingMaterial;
    [SerializeField]
    private Material constructingErrorMaterial;

	// Use this for initialization
	void Start () {	
        panel.SetActive(false);
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
            panel.SetActive(false);
            Cursor.visible = false;
            playerCameraController.movingCamera = true;
        }

        if(!panelEnabled && Input.GetKeyDown(KeyCode.Tab)){
            panel.SetActive(true);
            panelEnabled = true;
            Cursor.visible = true;
            playerCameraController.movingCamera = false;
        }

        if (isBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Rect rect = new Rect(hit.point.x, hit.point.z, 1, 1);

                instanciatedBuilding.transform.position = new Vector3(hit.point.x - hit.point.x % 10, 0, hit.point.z - hit.point.z % 10);
            }

            if(Input.GetMouseButtonDown(0)){
                isBuilding = false;
                Renderer rend = instanciatedBuilding.transform.GetChild(0).GetComponent<Renderer>();
                if (rend == null)
                {
                    rend = instanciatedBuilding.transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
                    if (rend == null)
                        return;
                }
                rend.material = defaultMaterial;
            }
        }

	}

    public void constructBuilding(GameObject building){
        isBuilding = true;
        instanciatedBuilding = Instantiate(building, new Vector3(0, 0, 0), Quaternion.identity);
        Renderer rend = instanciatedBuilding.transform.GetChild(0).GetComponent<Renderer>();
        if (rend == null)
        {
            rend = instanciatedBuilding.transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
            if (rend == null)
                return;
        }
        defaultMaterial = rend.material;
        setMaterialAvailable();
    } 
}
