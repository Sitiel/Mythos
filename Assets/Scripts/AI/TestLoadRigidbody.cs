using UnityEngine;
using System.Collections;

public class TestLoadRigidbody : MonoBehaviour
{
    public Unit unit;

	private void Update()
	{
       
        //Because of UMA creating bones in runtime this script help to attach weapons to bones by waiting for them to be ready
        if (GetComponentInChildren<SkinnedMeshRenderer>() != null){
            unit.enabled = true;
            unit.Ready();
            this.enabled = false;
        }

	}

	public void Ready(){

    }
}
