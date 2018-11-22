using UnityEngine;
using System.Collections;

public class Barrack : Building
{

    float unitCreationTimer = 5f;
    float currentTimer = 5f;

    public GameObject unitToCreate;
    public Transform spawnPoint;

    public override void Start()
    {
    }

	// Update is called once per frame
	void Update()
	{
        if(isBuild){
            currentTimer -= Time.deltaTime;
            if(currentTimer <= 0){
                currentTimer = unitCreationTimer;
                Debug.Log("Spawn at -> " + spawnPoint.name);
                Debug.Log(spawnPoint.position);
                Instantiate(unitToCreate, spawnPoint.position, Quaternion.identity);
            }
        }
	}
}
