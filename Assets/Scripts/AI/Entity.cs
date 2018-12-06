using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Base class of Buildings and Units in the game to allow life modification
public class Entity : MonoBehaviour {


    protected Animator animator;
    protected Rigidbody body;
    protected AudioSource source;

    public Dictionary<Entity, float> invulnerability = new Dictionary<Entity, float>();
    public List<AudioClip> hitSounds;


	// Use this for initialization
	public virtual void Start () {
        body = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
	}

    public virtual void Ready(){
        body = GetComponent<Rigidbody>();
    }


    //Attributes of the player related to the game
    public int life = 100;
    public int maxLife = 100;
    [HideInInspector]
    public bool isDead = false;

    private float lastDamage = 0f;
    private float lastHeal = 0f;


	public virtual void Update()
	{
        //We heal the entity if she have not taken any damage for 10 seconds, she will gain 5hp foreach seconds
        if(lastDamage+10f < Time.time && lastHeal + 1f < Time.time && life != maxLife){
            updateLife(new Damage(5, this));
        }
	}

	#region event

	public virtual void updateLife(Damage d)
    {
        if (isDead)
            return;

        //We do not allow entity to take damage from the same entity more than one time per second
        //invulnerability is a Dictionary/Hashmap for performance reason, we are avoiding to iterate around a list
        if(!invulnerability.ContainsKey(d.caller) || invulnerability[d.caller] + 1f < Time.time){
            if (d.nbDamage < 0)
            {
                lastDamage = Time.time;
                if (hitSounds.Count > 0){
                    //Play a damage sound randomly
                    source.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Count)]);
                }


            }
            else{
                lastHeal = Time.time;
            }
            
            this.life = Mathf.Clamp(this.life + d.nbDamage, 0, maxLife);
            isDead = this.life == 0;
            //Add the last time we got hit by this entity
            if (!invulnerability.ContainsKey(d.caller))
                invulnerability.Add(d.caller, Time.time);
            else
                invulnerability[d.caller] = Time.time;
        }
    }


    #endregion

}
