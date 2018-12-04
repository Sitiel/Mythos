using UnityEngine;
using System.Collections;

public class Barrack : Building
{

    public float unitCreationTimer = 30f;
    float currentTimer;

    public GameObject unitToCreate;
    public Transform spawnPoint;
    public LayerMask obstructSpawn;
    private GameResources resources;

    public override void Start()
    {
        currentTimer = unitCreationTimer;
    }

	public override void build()
	{
        base.build();
        resources = FindObjectOfType<GameResources>();
	}

	// Update is called once per frame
	public override void Update()
    {
        base.Update();
        if(isBuild){
            currentTimer -= Time.deltaTime;
            if(currentTimer <= 0 && resources.food > 0){
                resources.updateFood(-1);
                resources.updateWood(-10);
                resources.updateStone(-10);
                currentTimer = unitCreationTimer;
                Vector3 pos = spawnPoint.position + new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f, 1f));
                Instantiate(unitToCreate, pos, Quaternion.identity);
            }
        }
	}
}
