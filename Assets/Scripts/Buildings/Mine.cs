using UnityEngine;
using System.Collections;

public class Mine : Building
{
    public float stoneCreationTimer = 5f;
    public int stoneCreation = 1;
    float currentTimer;
    GameResources resources;
    public LayerMask rocks;

    public override void Start()
    {
        currentTimer = stoneCreationTimer;
        resources = FindObjectOfType<GameResources>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (isBuild)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                currentTimer = stoneCreationTimer;
                resources.updateStone(stoneCreation);
            }
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        Debug.Log("Enter : " + other.name);
        if (rocks == (rocks | (1 << other.gameObject.layer)))
        {
            stoneCreation = 5;
        }
	}
}
