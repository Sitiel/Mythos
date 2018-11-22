using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity {

    public int woodCost = 0;
    public int stoneCost = 0;
    public int foodCost = 0;
    public bool isBuild = false;


    private Resources resources;
    
    public override void Start () {
        
	}

    public void build(){
        resources = FindObjectOfType<Resources>();

        if (woodCost != 0)
        {
            resources.updateWood(-woodCost);
        }

        if (stoneCost != 0)
        {
            resources.updateStone(-stoneCost);
        }

        if (foodCost != 0)
        {
            resources.updateFood(-foodCost);
        }
        isBuild = true;
    }

    public override void updateLife(Damage d)
    {
        base.updateLife(d);
        if(isDead){
            Destroy(this.gameObject);
        }
    }
}
