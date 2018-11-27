using UnityEngine;
using System.Collections;

public class Mine : Building
{
    public float stoneCreationTimer = 5f;
    float currentTimer;
    GameResources resources;

    public override void Start()
    {
        currentTimer = stoneCreationTimer;
        resources = FindObjectOfType<GameResources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuild)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                currentTimer = stoneCreationTimer;
                resources.updateStone(1);
            }
        }
    }
}
