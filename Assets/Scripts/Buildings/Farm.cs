using UnityEngine;
using System.Collections;

public class Farm : Building
{

    GameResources resources;

    public override void Start()
    {
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

}
