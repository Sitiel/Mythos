using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour {

    public AudioSource rainSource;

    private void Start()
    {
        rainSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "trigger")
        {
            rainSource.Play();
            Destroy(col.gameObject);
        }
    }
}
