using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that will handle damage dealing from allie to ennemy and ennemy to allie
public class DamageDealer : MonoBehaviour {

    public LayerMask possiblesTargets;
    public int damage=1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void OnTriggerEnter(Collider other)
	{
        if(possiblesTargets == (possiblesTargets | (1 << other.gameObject.layer))){
            other.SendMessage("updateLife", -damage);
        }
	}
}
