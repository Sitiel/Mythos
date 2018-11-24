using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour {

    [SerializeField]
    ThirdPersonCameraController cameraController;
    [SerializeField]
    GameObject area;
    [SerializeField]
    public LayerMask camOcclusion;

    private bool selecting = false;
    private Vector3 selectionPos;
    private GameObject instanciedArea;
    private List<SimpleFollowerAI> alliesFollowing = new List<SimpleFollowerAI>();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C)) {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height*(2f/3), 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000, camOcclusion))
            {
                foreach(SimpleFollowerAI follower in alliesFollowing){
                    follower.unFollow();
                }
                alliesFollowing.Clear();

                selecting = true;
                selectionPos = hit.point;
                instanciedArea = Instantiate(area, selectionPos, Quaternion.identity);
            }
        }

        if (selecting)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height *(2f/3), 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,10000 ,camOcclusion))
            {
                float distance = Vector3.Distance(selectionPos, hit.point);
                instanciedArea.transform.localScale = new Vector3(distance, 1, distance);
            }


        }
        if(Input.GetKeyUp(KeyCode.C) && selecting){
            selecting = false;
            Destroy(instanciedArea);
            //Can probably improve by using unity collision rather than looping through all (R trees optimisation)
            GameObject[] allies = GameObject.FindGameObjectsWithTag("Allie");
            foreach (GameObject allie in allies)
            {
                if(Vector3.Distance(allie.transform.position, selectionPos) <= instanciedArea.transform.localScale.x){
                    SimpleFollowerAI allieAI = allie.GetComponent(typeof(SimpleFollowerAI)) as SimpleFollowerAI;
                    allieAI.follow();
                    alliesFollowing.Add(allieAI);
                }
            }
        }
	}
}
