using UnityEngine;
using System.Collections;

public class TestLoadRPG : MonoBehaviour
{
    public RPGCharacterController rpg;
	// Use this for initialization
	void Start()
	{

	}

	private void Update()
	{
       
        if (GetComponent<Rigidbody>() != null){
            rpg.enabled = true;
            rpg.Ready();
            this.enabled = false;
        }

	}

	public void Ready(){

    }
}
