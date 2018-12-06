using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPoison : MonoBehaviour {

    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Stop();
    }

	public void Update()
	{
        //If the boss is here the sword show the magic
        if(GlobalVariables.boss != null){
            particle.Play();
        }
	}
}
