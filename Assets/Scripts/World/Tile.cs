using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField]
    public int type = 0;
    [SerializeField]
    private List<GameObject> tilesTypeToObject;


	void Start () {
        GameObject prefabTile = Instantiate(tilesTypeToObject[type]) as GameObject;
        // X and Y corresponding to 2D top down str value
        prefabTile.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
	}
	
	void Update () {
		
	}
}
