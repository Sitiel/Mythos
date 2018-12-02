using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscallator : MonoBehaviour {

    float timeCounter = 0;
    float width;
    float speed;
    float height;

    void Start()
    {
        speed = 1;
        width = 200;
        height = 200;
    }

    void Update()
    {
        timeCounter += Time.deltaTime*speed;

        float x = Mathf.Cos(timeCounter)*width;
        float y = Mathf.Sin(timeCounter)*height;
        float z = Mathf.Cos(timeCounter)*width;

        transform.position = new Vector3(x, y, z);
    }
}
