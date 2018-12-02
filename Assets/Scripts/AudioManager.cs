using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {


    public AudioMixerSnapshot outOfCombat;
    public AudioMixerSnapshot inCombat;
    public AudioClip[] stings;
    public AudioSource stingSource;
    public float bpm = 128;


    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;

    private bool inFight = false;

    // Use this for initialization
    void Start()
    {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        m_TransitionOut = m_QuarterNote * 32;

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

    void PlaySting()
    {
        int randClip = Random.Range(0, stings.Length);
        stingSource.clip = stings[randClip];
        stingSource.Play();
    }

}
