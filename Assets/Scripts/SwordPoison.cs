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

    private void OnTriggerEnter(SwordPoison col)
    {
        if (col.gameObject.tag == "Hydra")
        {
            particle.Play();
        }

    }

    private void OnTriggerExit(SwordPoison col)
    {
        if (col.gameObject.tag == "Hydra")
        {
            particle.Stop();
        }
    }
}
