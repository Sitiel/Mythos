using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUnit : MonoBehaviour {


    protected Animator animator;

	// Use this for initialization
	void Start () {
	}


    //Attributes of the player related to the game
    public int life = 100;
    public int maxLife = 100;
    public bool isDead = false;


    #region event

    public void updateLife(int value)
    {
        if (isDead)
            return;
        
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
