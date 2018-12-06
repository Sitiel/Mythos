using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPoison : MonoBehaviour {

    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }

	public void Update()
	{
        if(GlobalVariables.boss != null){
            particle.Play();
        }
	}
}
