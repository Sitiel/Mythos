using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HydraAI : Unit
{

    private NavMeshAgent agent;

    public GameObject townHall;
    private GameObject currentTarget;
    private EntityTargetFinder finder;
    private bool canAttack = true;


    public override void Start()
    {
        base.Start();
        animator = GetComponent(typeof(Animator)) as Animator;
        agent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        finder = GetComponentInChildren(typeof(EntityTargetFinder)) as EntityTargetFinder;
        townHall = GlobalVariables.townHall;

    }


    public void lookAround()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        List<Entity> nearbyTargets = finder.getEntitiesInArea();
        if (nearbyTargets.Count == 0){
            return;
        }
       
        
        foreach (Entity potentialTarget in nearbyTargets)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = getRealDistBetweenGameObject(potentialTarget.gameObject);
            if (dSqrToTarget < closestDistanceSqr && dSqrToTarget > 13.5f)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.gameObject;
            }
        }
        if(bestTarget != null)
            currentTarget = bestTarget.gameObject;

    }


	public void findATarget()
    {
        //Default target
        currentTarget = townHall;
        //Ennemy look around if the player, building or allie to the player is nearby
        lookAround();
    }



    // Update is called once per frame
    void Update()
    {
        if(isDead){
            agent.isStopped = true;
            return;
        }
        findATarget();
        if (currentTarget == null)
            return;
        
        agent.destination = getNearestPointTo(currentTarget);

        if(getRealDistBetweenGameObject(currentTarget) < 14.5f && getRealDistBetweenGameObject(currentTarget) > 13.5f && canAttack){
            this.transform.LookAt(currentTarget.transform.position, Vector3.up);
            animator.SetTrigger("Bite");
            agent.isStopped = true;
            agent.velocity = new Vector3(0, 0, 0);
            canAttack = false;
            StartCoroutine(_WaitEndOfAttack(1f));
            //animator.ResetTrigger("attack");
        }
        else{
            animator.SetTrigger("Move");
        }
    }

    public IEnumerator _WaitEndOfAttack(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
        agent.isStopped = false;

    }
}
