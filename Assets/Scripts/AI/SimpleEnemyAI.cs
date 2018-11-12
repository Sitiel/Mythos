using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAI : MonoBehaviour {

    public NavMeshAgent agent;
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}

    public void MoveToLocation(Vector3 targetPoint)
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        agent.destination = player.transform.position;
        agent.isStopped = false;
	}
}
