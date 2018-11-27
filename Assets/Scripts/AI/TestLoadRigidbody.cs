using UnityEngine;
using System.Collections;

public class TestLoadRigidbody : MonoBehaviour
{
    public Unit unit;

	private void Update()
	{
       
        if (GetComponent<Rigidbody>() != null){
            unit.enabled = true;
            unit.Ready();
            this.enabled = false;
        }

	}

	public void Ready(){

    }
}
