using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAI : Unit
{

    private NavMeshAgent agent;

    public GameObject townHall;
    private GameObject currentTarget;
    private UnitTargetFinder finder;
    private bool canAttack = true;


    public override void Start()
    {
        base.Start();
        animator = GetComponent(typeof(Animator)) as Animator;
        agent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        finder = GetComponentInChildren(typeof(UnitTargetFinder)) as UnitTargetFinder;
    }


    public void lookAround()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;


        List<Unit> nearbyTargets = finder.getUnitsInArea();
        if (nearbyTargets.Count == 0){
            return;
        }
            
       
        
        foreach (Unit potentialTarget in nearbyTargets)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = getRealDistBetweenGameObject(potentialTarget.gameObject);
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.gameObject;
            }
        }
        currentTarget = bestTarget.gameObject;

    }


	public void findATarget()
    {
        //Default target
        currentTarget = townHall;
        //Ennemy look around if the player, building or allie to the player is nearby
        lookAround();
    }

    private Vector3 getNearestPointTo(GameObject g){
        Collider c = g.GetComponent<Collider>();
        return c.ClosestPointOnBounds(this.transform.position);
    }


    private float getRealDistBetweenGameObject(GameObject b){

        //Calculate distance between the 2 colliders
        return Vector3.Distance(this.transform.position, getNearestPointTo(b));
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead){
            agent.isStopped = true;
            return;
        }
        findATarget();
        agent.destination = getNearestPointTo(currentTarget);


        //Debug.Log("Distance : " + getRealDistBetweenGameObject(currentTarget) + " from : " + currentTarget.name + " -> " + getNearestPointTo(currentTarget) + " vs " + this.transform.position);
        if(getRealDistBetweenGameObject(currentTarget) < 1.5f && canAttack){
            animator.SetTrigger("Attack1Trigger");
            agent.isStopped = true;
            canAttack = false;
            StartCoroutine(_WaitEndOfAttack(0.75f));
            //animator.ResetTrigger("attack");

        }


        animator.SetBool("Moving", (agent.velocity != (new Vector3(0,0,0))));
        float velocityXel = transform.InverseTransformDirection(agent.velocity).x;
        float velocityZel = transform.InverseTransformDirection(agent.velocity).z;

        //Update animator with movement values
        animator.SetFloat("Velocity X", velocityXel);
        animator.SetFloat("Velocity Z", velocityZel);
    }

    public IEnumerator _WaitEndOfAttack(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
        agent.isStopped = false;

    }
}
