using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Class for the Hydra AI
// It is a simple AI, just going to the nearest ennemy that she can hit (to avoid being stuck with ennemy around her feet
public class HydraAI : Unit
{

    private NavMeshAgent agent;

    public GameObject townHall;
    private GameObject currentTarget;
    private EntityTargetFinder finder;
    private bool canAttack = true;
    public AudioClip roar;
    public AudioClip attack;



    public override void Start()
    {
        base.Start();
        animator = GetComponent(typeof(Animator)) as Animator;
        agent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        finder = GetComponentInChildren(typeof(EntityTargetFinder)) as EntityTargetFinder;
        townHall = GlobalVariables.townHall;
        source.PlayOneShot(roar);
        GlobalVariables.boss = this;
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
       

        //Loop through ennemies around who are the closest but in range
        foreach (Entity potentialTarget in nearbyTargets)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = getRealDistBetweenGameObject(potentialTarget.gameObject);
            if (dSqrToTarget < closestDistanceSqr && dSqrToTarget > 7.5f)
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
    public override void Update()
    {
        base.Update();
        if(isDead){
            agent.isStopped = true;
            return;
        }
        findATarget();
        if (currentTarget == null)
            return;
        
        agent.destination = getNearestPointTo(currentTarget);
        if(getRealDistBetweenGameObject(currentTarget) < 9f && getRealDistBetweenGameObject(currentTarget) > 7.5f && canAttack){
            this.transform.LookAt(currentTarget.transform.position, Vector3.up);
            source.PlayOneShot(attack);
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

    public override void updateLife(Damage d)
    {
        //Only the player with the magic sword can make damage to the hydra, weapon 4 = magic sword
        if(d.caller is RPGCharacterController && GlobalVariables.player.inventory.GetCurrentWeapon() == 4){
            base.updateLife(d);
        }
        if (isDead)
        {
            FindObjectOfType<UIManager>().youWin();
        }
    }

    //Using coroutine to wait for the end of the attack
    public IEnumerator _WaitEndOfAttack(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
        agent.isStopped = false;

    }
}
