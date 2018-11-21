using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Unit {
    
    public override void Start () {
		
	}

    public override void updateLife(int value)
    {
        this.life += value;
        if(this.life < 0){
            this.isDead = true;
            Destroy(this.gameObject);
        }
    }
}
