using UnityEngine;
using System.Collections;

public class LumberjackHut : Building
{
    public float woodCreationTimer = 10f;
    public int woodCreation = 1;
    float currentTimer;
    GameResources resources;
    public float treesDistances = 20;
    private Terrain terrain;

    public override void Start()
    {
        currentTimer = woodCreationTimer;
        resources = FindObjectOfType<GameResources>();
    }


	public override void build()
	{
		base.build();
        terrain = FindObjectOfType<Terrain>();
        TreeInstance[] trees = terrain.terrainData.treeInstances;
        for (int i = 0; i < trees.Length; i++){
            Vector3 realPos = Vector3.Scale(trees[i].position, terrain.terrainData.size) + terrain.transform.position;
            // Debug.Log("Distance : " + Vector3.Distance(this.transform.position, realPos));
            if(Vector3.Distance(this.transform.position, realPos) <= treesDistances){
                woodCreation++;
            }
        }

	}

	// Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(isBuild){
            currentTimer -= Time.deltaTime;
            if(currentTimer <= 0){
                currentTimer = woodCreationTimer;
                resources.updateWood(woodCreation/2);
            }
        }
    }
}
