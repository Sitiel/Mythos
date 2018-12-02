using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour {

    public AudioSource rainSource;

    ParticleSystem particle;

    private void Start()
    {
        rainSource = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
        rainSource.Stop();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            rainSource.Play();
            particle.Play();
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            rainSource.Stop();
            particle.Stop();
        }
    }
}
