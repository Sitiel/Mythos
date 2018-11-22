using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityTargetFinder : MonoBehaviour
{

    private List<Entity> nearbyTargets = new List<Entity>();
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
            if (other.gameObject.GetComponent<Entity>() != null){
                nearbyTargets.Add(other.gameObject.GetComponent<Entity>());
            }
                
        }

    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Entity t in nearbyTargets)
        {
            if (other.gameObject == t.gameObject)
            {
                nearbyTargets.Remove(t);
                return;
            }
        }
    }

    public List<Entity> getEntitiesInArea(){
        nearbyTargets.RemoveAll(u => u.isDead);
        return nearbyTargets;
    }
}
