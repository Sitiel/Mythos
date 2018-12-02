using UnityEngine;
using System.Collections;

public class Barrack : Building
{

    public float unitCreationTimer = 5f;
    float currentTimer;

    public GameObject unitToCreate;
    public Transform spawnPoint;

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
                Debug.Log("Spawn");
                currentTimer = unitCreationTimer;
                Instantiate(unitToCreate, spawnPoint.position, Quaternion.identity);
            }
        }
	}
}
