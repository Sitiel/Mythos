using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour {

    private List<List<Tile>> tileWorld;
    GameObject tileCreator;


	// Use this for initialization
	void Start () {

	}

    public void loadWorld(List<List<Tile>> tileWorld){
        int x = 0;
        int y = 0;

        foreach (List<Tile> line in tileWorld)
        {
            foreach (Tile tile in line)
            {
                tile.transform.position = new Vector3(x*10, tile.y, y*10);
                //prefabTile.GetComponent<Tile>().type = tile;
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
