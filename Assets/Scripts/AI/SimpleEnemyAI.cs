using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAI : AIUnit
{

    private NavMeshAgent agent;

    public GameObject townHall;
    private Vector3 currentTarget;
    private List<AIUnit> nearbyTargets = new List<AIUnit>();
    public LayerMask possiblesTargets;

    void Start()
    {
        animator = GetComponent(typeof(Animator)) as Animator;
        agent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (possiblesTargets == (possiblesTargets | (1 << other.gameObject.layer)))
        {
            nearbyTargets.Add(other.gameObject.GetComponent<AIUnit>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (AIUnit t in nearbyTargets)
        {
            if (other.gameObject == t.gameObject)
            {
                nearbyTargets.Remove(t);
                return;
            }
        }
    }


    public void lookAround()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        nearbyTargets.RemoveAll(u => u.isDead);

        if (nearbyTargets.Count == 0)
            return;
        
        foreach (AIUnit potentialTarget in nearbyTargets)
        {
            if(potentialTarget.isDead){
                
            }
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        currentTarget = bestTarget.position;

    }


	public void findATarget()
    {
        //Default target
        currentTarget = townHall.transform.position;
        //Ennemy look around if the player, building or allie to the player is nearby
        lookAround();
    }

    // Update is called once per frame
    void Update()
    {
        findATarget();
        agent.destination = currentTarget;

        if(Vector3.Distance(currentTarget, transform.position) < 10){
            animator.SetTrigger("attack");
            //animator.ResetTrigger("attack");

        }


        animator.SetBool("moving", (agent.velocity != (new Vector3(0,0,0))));
    }
}
