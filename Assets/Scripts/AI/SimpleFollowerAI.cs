using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleFollowerAI : Unit {

    float moveSpeed = 7; 
    float rotationSpeed = 10;
    bool isFollowing = false;
    [SerializeField]
    GameObject player;
    private Rigidbody rb;

    bool canAttack = true;

    private NavMeshAgent agent;
    private UnitTargetFinder finder;

	// Use this for initialization
    public override void Start () {
        base.Start();
        animator = GetComponent(typeof(Animator)) as Animator;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        finder = GetComponentInChildren(typeof(UnitTargetFinder)) as UnitTargetFinder;

        
    }


    public void follow(){
        animator.SetBool("Moving", true);
        isFollowing = true;
        agent.isStopped = false;
    }

    public void unFollow(){
        animator.SetBool("Moving", false);
        isFollowing = false;
        agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            agent.isStopped = true;
        }
        else
        {
            List<Unit> nearbyEnnemies = finder.getUnitsInArea();
            if(nearbyEnnemies.Count != 0){
                agent.destination = nearbyEnnemies[0].transform.position;
                if(Vector3.Distance(nearbyEnnemies[0].transform.position, transform.position) < 5 && canAttack){
                    animator.SetTrigger("Attack1Trigger");
                    agent.isStopped = true;
                    canAttack = false;
                    StartCoroutine(_WaitEndOfAttack(0.75f));
                }
            }
            else if (isFollowing)
            {
                agent.isStopped = false;
                agent.destination = player.transform.position;
            }
            else{
                //agent.isStopped = true;
            }


            //Get local velocity of charcter
            float velocityXel = transform.InverseTransformDirection(agent.velocity).x;
            float velocityZel = transform.InverseTransformDirection(agent.velocity).z;

            //Update animator with movement values
            animator.SetFloat("Velocity X", velocityXel);
            animator.SetFloat("Velocity Z", velocityZel);

           
        }
    }

    public IEnumerator _WaitEndOfAttack(float time){
        yield return new WaitForSeconds(time);
        canAttack = true;
        agent.isStopped = false;

    }
}
