using UnityEngine;
using System.Collections;

public class Barrack : Building
{

    public float unitCreationTimer = 5f;
    float currentTimer;

    public GameObject unitToCreate;
    public Transform spawnPoint;
    public LayerMask obstructSpawn;

    public override void Start()
    {
        currentTimer = unitCreationTimer;
    }

	// Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(isBuild){
            currentTimer -= Time.deltaTime;
            if(currentTimer <= 0){
                currentTimer = unitCreationTimer;
                Vector3 pos = spawnPoint.position + new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f, 1f));
                Instantiate(unitToCreate, pos, Quaternion.identity);
            }
        }
	}
}
