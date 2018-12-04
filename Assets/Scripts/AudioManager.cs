using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {


    public AudioMixerSnapshot outOfCombat;
    public AudioMixerSnapshot inCombat;
    public AudioMixerSnapshot rainDrop;
    public AudioClip[] stings;
    public AudioSource stingSource;


    private float m_TransitionIn;
    private float m_TransitionOut;

    private bool inFight = false;

    // Use this for initialization
    void Start()
    {
        m_TransitionIn = 1;
        m_TransitionOut = 3;

    }

	private void Update()
	{
        SimpleEnemyAI enemyAI = FindObjectOfType<SimpleEnemyAI>();
        if (enemyAI != null && !inFight){
            startFight();
        }
        else if(enemyAI == null && inFight){
            endFight();
        }
	}

	void startFight()
    {
         inCombat.TransitionTo(m_TransitionIn);
        //PlaySting();
        inFight = true;
    }

    void endFight()
    {
        outOfCombat.TransitionTo(m_TransitionOut);
        inFight = false;
    }

    public void startRain(){
        rainDrop.TransitionTo(5f);
    }

    public void endRain(){
        if(inFight){
            inCombat.TransitionTo(m_TransitionIn);
        }else{
            outOfCombat.TransitionTo(m_TransitionOut);
        }
    }

    void PlaySting()
    {
        int randClip = Random.Range(0, stings.Length);
        stingSource.clip = stings[randClip];
        stingSource.Play();
    }

}
