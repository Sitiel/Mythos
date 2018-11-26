using UnityEngine;
using System.Collections;

public class Farm : Building
{
    public float foodCreationTimer = 5f;
    float currentTimer;
    GameResources resources;

    public override void Start()
    {
        currentTimer = foodCreationTimer;
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
                currentTimer = foodCreationTimer;
                resources.updateFood(1);
            }
        }
    }
}
