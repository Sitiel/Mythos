using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitTargetFinder : MonoBehaviour
{

    private List<Unit> nearbyTargets = new List<Unit>();
    public LayerMask possiblesTargets;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
			
	}

    private void OnTriggerEnter(Collider other)
    {
        if (possiblesTargets == (possiblesTargets | (1 << other.gameObject.layer)))
        {
            if (other.gameObject.GetComponent<Unit>() != null){
                nearbyTargets.Add(other.gameObject.GetComponent<Unit>());
            }
                
        }

    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Unit t in nearbyTargets)
        {
            if (other.gameObject == t.gameObject)
            {
                nearbyTargets.Remove(t);
                return;
            }
        }
    }

    public List<Unit> getUnitsInArea(){
        nearbyTargets.RemoveAll(u => u.isDead);
        return nearbyTargets;
    }
}
