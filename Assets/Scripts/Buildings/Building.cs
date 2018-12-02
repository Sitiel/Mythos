using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : Entity {

    public int woodCost = 0;
    public int stoneCost = 0;
    public int foodCost = 0;
    public bool isBuild = false;
    private Slider lifeSlider;


    private GameResources resources;

    
    public override void Start () {
	}

    public virtual void build(){
        resources = FindObjectOfType<GameResources>();
        lifeSlider = this.transform.parent.GetComponentInChildren<Slider>();

        if(resources.wood < woodCost || resources.food < foodCost || resources.stone < stoneCost){
            Destroy(this.gameObject);
            return;
        }

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

        if(lifeSlider != null)
            lifeSlider.value = (float)(life)/maxLife;

        if(isDead){
            GetComponent<Collider>().enabled = false;
            FracturedObject f = transform.parent.GetComponentInChildren<FracturedObject>();
            f.SupportChunksAreIndestructible = false;
            f.Explode(new Vector3(transform.position.x, 0, transform.position.z), 100f, 100f, false, false,false,false);
            Destroy(this.gameObject, 30);
        }
    }
}
