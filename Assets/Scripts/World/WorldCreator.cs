using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour {

    [SerializeField]
    private List<NestedListWrapper> tileWorld;
    [SerializeField]
    GameObject tileCreator;
    [SerializeField]
    bool useWorldGenerator = true;


	// Use this for initialization
	void Start () {
        if(!useWorldGenerator){
            loadWorld(tileWorld);
        }
	}

    public void loadWorld(List<NestedListWrapper> tileWorld){
        int x = 0;
        int y = 0;

        foreach (NestedListWrapper line in tileWorld)
        {
            foreach (int tile in line.list)
            {
                GameObject prefabTile = Instantiate(tileCreator) as GameObject;
                prefabTile.transform.position = new Vector3(x, 1, y);
                prefabTile.GetComponent<Tile>().type = tile;
                y++;
            }
            x++;
            y = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
