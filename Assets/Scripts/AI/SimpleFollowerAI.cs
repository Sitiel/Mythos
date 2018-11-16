using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleFollowerAI : AIUnit {

    float moveSpeed = 7; 
    float rotationSpeed = 10;
    bool isFollowing = false;
    [SerializeField]
    GameObject player;
    private Rigidbody rb;

    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        animator = GetComponent(typeof(Animator)) as Animator;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        
    }


    public void follow(){
        animator.SetBool("Moving", true);
        Debug.Log("Moving");
        isFollowing = true;
        agent.isStopped = false;
    }

    public void unFollow(){
        animator.SetBool("Moving", false);
        Debug.Log("Not Moving");
        isFollowing = false;
        agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
        }
        else
        {
            if (isFollowing)
            {
                agent.destination = player.transform.position;

                //Get local velocity of charcter
                float velocityXel = transform.InverseTransformDirection(agent.velocity).x;
                float velocityZel = transform.InverseTransformDirection(agent.velocity).z;

                //Update animator with movement values
                animator.SetFloat("Velocity X", velocityXel);
                animator.SetFloat("Velocity Z", velocityZel);

                /*Quaternion onlyY = Quaternion.Euler(0, 1, 0);
                transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position)*onlyY, rotationSpeed * Time.deltaTime);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;*/
            }
        }
    }
}
