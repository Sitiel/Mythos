using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscallator : MonoBehaviour {

    float timeCounter = 0;
    float width;
    float speed;
    float height;
    float rotation = 0;

    void Start()
    {
        speed = 3f;
        width = 400;
        height = 200;
    }

    void Update()
    {
        /*timeCounter += Time.deltaTime*speed;

        float x = Mathf.Cos(timeCounter)*width;
        float y = Mathf.Sin(timeCounter)*height;
        float z = Mathf.Cos(timeCounter)*width;

        transform.position = new Vector3(x, y, z); */
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
        rotation += Time.deltaTime * speed;
    }
}
