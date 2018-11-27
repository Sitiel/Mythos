using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {


    protected Animator animator;
    protected Rigidbody body;

    public Dictionary<Entity, float> invulnerability = new Dictionary<Entity, float>();

	// Use this for initialization
	public virtual void Start () {
        body = GetComponent<Rigidbody>();
	}

    public virtual void Ready(){
        body = GetComponent<Rigidbody>();
    }


    //Attributes of the player related to the game
    public int life = 100;
    public int maxLife = 100;
    [HideInInspector]
    public bool isDead = false;


    #region event

    public virtual void updateLife(Damage d)
    {
        if (isDead)
            return;
        if(!invulnerability.ContainsKey(d.caller) || invulnerability[d.caller] + 1f < Time.time){;
            this.life = Mathf.Clamp(this.life + d.nbDamage, 0, maxLife);
            isDead = this.life == 0;
            if (!invulnerability.ContainsKey(d.caller))
                invulnerability.Add(d.caller, Time.time);
            else
                invulnerability[d.caller] = Time.time;
        }
    }

    #endregion

}
