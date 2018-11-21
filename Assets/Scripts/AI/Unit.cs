using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {


    protected Animator animator;
    protected Rigidbody body;

	// Use this for initialization
	public virtual void Start () {
        body = GetComponent<Rigidbody>();
	}


    //Attributes of the player related to the game
    public int life = 100;
    public int maxLife = 100;
    [HideInInspector]
    public bool isDead = false;


    #region event

    public virtual void updateLife(int value)
    {
        body.velocity = new Vector3(0, 0, 0);
        if (isDead)
        {
            return;
        }
        
        this.life = Mathf.Clamp(this.life + value, 0, maxLife);
        if (value < 0)
        {
            animator.SetTrigger("GetHit3Trigger");
        }

        if(this.life == 0)
        {
            animator.SetTrigger("Death1Trigger");
            isDead = true;
            Destroy(this.gameObject,30f);
        }


    }

    #endregion
	
	// Update is called once per frame
	void Update () {
		
	}
}
