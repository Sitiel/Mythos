using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetVolume (float volume)
    {
        Debug.Log("Moving volume : " + volume);
        audioMixer.SetFloat("volume", volume);
    }

}
