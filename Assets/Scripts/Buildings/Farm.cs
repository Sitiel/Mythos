using UnityEngine;
using System.Collections;

public class Farm : Building
{

    GameResources resources;
    public float woodCostTimer = 10f;
    public int woodCostT = 5;
    float currentTimer;

    public override void Start()
    {
        //The farm is a special building because to make the game run better I'm limiting the units that you can create by limiting the food
        //This building only give one time food to limit player army
        resources = FindObjectOfType<GameResources>();
        resources.updateFood(5);
    }

	public override void updateLife(Damage d)
	{
        base.updateLife(d);
        if(life == 0){
            resources.updateFood(-5);
        }
	}

    public override void Update()
    {
        base.Update();
        if (isBuild)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                currentTimer = woodCostTimer;
                resources.updateWood(-woodCostT);
            }
        }
    }

}
