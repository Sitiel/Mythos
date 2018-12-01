using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleFollowerAI : Unit {

    float moveSpeed = 7; 
    float rotationSpeed = 10;
    bool isFollowing = false;
    GameObject player;
    private Rigidbody rb;

    bool canAttack = true;

    private NavMeshAgent agent;
    private EntityTargetFinder finder;
    public GameObject sword;

	// Use this for initialization
    public override void Start () {
        base.Start();
        animator = GetComponent(typeof(Animator)) as Animator;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        finder = GetComponentInChildren(typeof(EntityTargetFinder)) as EntityTargetFinder;
        player = FindObjectOfType<RPGCharacterController>().gameObject;
        animator.SetInteger("Weapon", 7);
        animator.SetInteger("RightWeapon", 9);
        animator.SetInteger("AttackSide", 2);
        
    }

    public override void Ready()
    {
        base.Ready();
        Transform rightHand = transform.Find("Root/Global/Position/Hips/LowerBack/Spine/Spine1/RightShoulder/RightArm/RightForeArm/RightHand");

        Vector3 old = sword.transform.localPosition;
        Quaternion oldQ = sword.transform.localRotation;
        Vector3 oldS = sword.transform.localScale;
        sword.transform.parent = rightHand;
        sword.transform.localPosition = old;
        sword.transform.localRotation = oldQ;
        sword.transform.localScale = oldS;
    }



    public void follow(){
        isFollowing = true;
        agent.isStopped = false;
    }

    public void unFollow(){
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
            List<Entity> nearbyEnnemies = finder.getEntitiesInArea();
            if(nearbyEnnemies.Count != 0){
                agent.destination = getNearestPointTo(nearbyEnnemies[0].gameObject);
                if (canAttack)
                {
                    agent.isStopped = false;
                }

                if(getRealDistBetweenGameObject(nearbyEnnemies[0].gameObject) < .5f && canAttack){
                    animator.SetTrigger("Attack8Trigger");
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

            animator.SetBool("Moving", (agent.velocity != (new Vector3(0, 0, 0))));
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
