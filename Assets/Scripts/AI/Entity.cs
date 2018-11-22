using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {


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
        this.life = Mathf.Clamp(this.life + value, 0, maxLife);
        isDead = this.life == 0;
    }

    #endregion

}
