﻿
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
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject youWinPanel;
    protected AudioSource source;

    public AudioClip openBook;
    public AudioClip closeBook;

    private bool panelEnabled = false;
    private bool isBuilding = false;
    private GameObject instanciatedBuilding;
    private Material defaultMaterial;
    public LayerMask camOcclusion;
    public LayerMask collideForConstruct;
    Build currentBuild;
    bool possibleToConstruct;


	// Use this for initialization
	void Start () {	
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bookPanel.SetActive(false);
        constructionPanel.SetActive(false);
        source = GetComponent<AudioSource>();
	}

    private void cancelBuild()
    {
        if (isBuilding)
        {
            isBuilding = false;
            Destroy(instanciatedBuilding.gameObject);
        }
    }

    public void gameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void youWin()
    {
        youWinPanel.SetActive(true);
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

                Vector3 size = instanciatedBuilding.GetComponentInChildren<Renderer>().bounds.size;
                Collider[] colliders = Physics.OverlapSphere(hit.point, size.z, collideForConstruct);
                
                if((hit.point.y > 0.3 || colliders.Length != 0 || Vector3.Distance(playerCameraController.transform.position, hit.point) >= 3 * size.z/*|| Vector3.Distance(playerCameraController.transform.position, hit.point) < size.z*/)){
                    if(possibleToConstruct){
                        Destroy(instanciatedBuilding);
                        instanciatedBuilding = Instantiate(currentBuild.preview_nok);
                        possibleToConstruct = false;
                    }
                }
                else if(hit.point.y <= 0.3 && !possibleToConstruct){
                    Destroy(instanciatedBuilding);
                    instanciatedBuilding = Instantiate(currentBuild.preview_ok);
                    possibleToConstruct = true;
                }
                instanciatedBuilding.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                Terrain terrain = FindObjectOfType<Terrain>();
                TreeInstance[] trees = terrain.terrainData.treeInstances;


                instanciatedBuilding.transform.rotation = Quaternion.Euler(instanciatedBuilding.transform.eulerAngles.x, playerCameraController.transform.eulerAngles.y, instanciatedBuilding.transform.eulerAngles.z);

            }


            if (Input.GetMouseButtonDown(0))
            {
                if(possibleToConstruct){
                    Vector3 lastPos = instanciatedBuilding.transform.position;
                    Quaternion lastRot = instanciatedBuilding.transform.rotation;
                    Destroy(instanciatedBuilding);
                    instanciatedBuilding = Instantiate(currentBuild.building, lastPos, lastRot);
                    isBuilding = false;

                    instanciatedBuilding.GetComponentInChildren<Building>().build();
                }
                else{
                    Destroy(instanciatedBuilding);
                }
               
            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(instanciatedBuilding);
                isBuilding = false;

            }
        }

        if(!panelEnabled && Input.GetKeyDown(KeyCode.J)){
            bookPanel.SetActive(!bookPanel.activeSelf);
            if(bookPanel.activeSelf){
                source.PlayOneShot(openBook);
            }
            else{
                source.PlayOneShot(closeBook);
            }
            Cursor.visible = bookPanel.activeSelf;
            if(bookPanel.activeSelf)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            
            playerCameraController.movingCamera = !bookPanel.activeSelf;
        }

	}

    public void constructBuilding(Build build){
        currentBuild = build;
        isBuilding = true;
        instanciatedBuilding = Instantiate(currentBuild.preview_nok);
        possibleToConstruct = false;
    } 


    public void quit(){
        Application.Quit();
    }
}
