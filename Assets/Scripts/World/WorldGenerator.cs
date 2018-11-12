using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    [SerializeField]
    int widthOfTheWorld = 100;
    [SerializeField]
    int lengthOfTheWorld = 100;

    [SerializeField]
    WorldCreator creator;

    [SerializeField]
    private Tile grass;
    [SerializeField]
    private Tile sand;
    [SerializeField]
    private Tile rock;


    [SerializeField]
    private List<GameObject> trees;

    [SerializeField]
    private List<GameObject> bushes;

    [SerializeField]
    private List<GameObject> rocks;

    [SerializeField]
    private List<GameObject> stones;

    private int strengthOfMountain = 5;
    private int areaOfMountain = 10;
    //private float 


    int tileDistance(Vector2 p1, Vector2 p2){
        return (int)(Mathf.Abs((p1.x - p2.x)) + Mathf.Abs((p1.y - p2.y)));
    }

    int getYOn2DPos(List<List<Tile>> world, Vector2 p){
        return world[(int)(p.x / 10)][(int)(p.y / 10)].y;
    }

	// Use this for initialization
	void Start () {

        List<Vector2> mountainsPositions = new List<Vector2>();
        List<Vector2> forestsPositions = new List<Vector2>();


        int nbMountains = Random.Range(2, 5);
        for (int i = 0; i < nbMountains; i++){
            mountainsPositions.Add(new Vector2(Random.Range(0, widthOfTheWorld), Random.Range(0, lengthOfTheWorld)));
        }


        List<List<Tile>> world = new List<List<Tile>>();
        for (int i = 0; i < widthOfTheWorld; i++){
            List<Tile> line = new List<Tile>();
            for (int j = 0; j < lengthOfTheWorld; j++){
                int tileY = 0;
                for (int m = 0; m < mountainsPositions.Count; m++){
                    int dist = tileDistance(mountainsPositions[m], new Vector2(i, j));
                    int value = areaOfMountain - dist;
                    if(dist < areaOfMountain && value > tileY){
                        tileY = (value + Random.Range(-1, 2)) * strengthOfMountain;
                    }
                }
                Tile tmp;
                if (tileY > 0)
                {
                    tmp = Instantiate(rock) as Tile;
                }
                else
                {
                    if(i < 2 || i >= widthOfTheWorld - 2 || j < 2 || j >= lengthOfTheWorld - 2){
                        tmp = Instantiate(sand) as Tile;
                    }
                    else{
                        tmp = Instantiate(grass) as Tile; 
                    }

                }

                tmp.y = tileY;
                line.Add(tmp);
            }
            world.Add(line);
        }

        int nbForests = Random.Range(2, 5);
        for (int i = 0; i < nbForests; i++)
        {
            Vector2 v = new Vector2(Random.Range(0, widthOfTheWorld), Random.Range(0, lengthOfTheWorld));
            bool redo = true;
            while(redo){
                redo = false;
                for (int k = 0; k < nbMountains; k++){
                    if (tileDistance(v, mountainsPositions[k]) <= areaOfMountain + 8){
                        v = new Vector2(Random.Range(0, widthOfTheWorld), Random.Range(0, lengthOfTheWorld));
                        redo = true;
                        break;
                    }
                }
            }

            int nbTrees = Random.Range(200, 500);
            for (int j = 0; j < nbTrees; j++)
            {
                float x = Random.Range(-80f, 80f);
                float y = Random.Range(-80f, 80f);

                GameObject t = Instantiate(trees[Random.Range(0, trees.Count)]) as GameObject;
                float rX = Mathf.Clamp(v.x * 10 + x, 0, widthOfTheWorld * 10), rY = Mathf.Clamp(v.y * 10 + y, 0, lengthOfTheWorld * 10);
                t.transform.position = new Vector3(rX, 0, rY);
                float size = Random.Range(0.5f, 1.5f);
                t.transform.localScale = new Vector3(size, size, size);
                t.transform.rotation = new Quaternion(Random.Range(0, 5), Random.Range(0, 360), Random.Range(0, 5), 0);
            }

            int nbBush = Random.Range(200, 500);
            for (int j = 0; j < nbBush; j++)
            {
                float x = Random.Range(-80f, 80f);
                float y = Random.Range(-80f, 80f);

                GameObject t = Instantiate(bushes[Random.Range(0, bushes.Count)]) as GameObject;
                float rX = Mathf.Clamp(v.x * 10 + x, 0, widthOfTheWorld * 10), rY = Mathf.Clamp(v.y * 10 + y, 0, lengthOfTheWorld * 10);
                t.transform.position = new Vector3(rX, 0, rY);
                float size = Random.Range(0.5f, 1.5f);
                t.transform.localScale = new Vector3(size, size, size);
            }

        }

        for (int k = 0; k < nbMountains; k++)
        {
            int nbRocks = Random.Range(25, 100);
            for (int l = 0; l < nbRocks; l++)
            {
                Vector2 rockPos = new Vector2(Random.Range(-80f, 80f), Random.Range(-80f, 80f));
                GameObject t = Instantiate(rocks[Random.Range(0, rocks.Count)]) as GameObject;
                Vector2 rRockPos = rockPos + (mountainsPositions[k] * 10);
                rRockPos = new Vector2(Mathf.Clamp(rRockPos.x, 0, widthOfTheWorld * 10-1), Mathf.Clamp(rRockPos.y, 0, lengthOfTheWorld * 10-1));
                t.transform.position = new Vector3(rRockPos.x, getYOn2DPos(world, rRockPos), rRockPos.y);
                float size = Random.Range(2f, 5f);
                t.transform.localScale = new Vector3(size, size, size);
                t.transform.rotation = new Quaternion(0, Random.Range(0, 360), 0, 0);

            }
        }


        creator.loadWorld(world);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
