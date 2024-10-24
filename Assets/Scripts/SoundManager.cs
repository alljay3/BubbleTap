using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float volume;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip audioClip)
    {
        gameObject.GetComponent<AudioSource>().clip = audioClip;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void Mute()
    {
        gameObject.GetComponent<AudioSource>().volume = 0;
    }

    public void UnMute()
    {
        gameObject.GetComponent<AudioSource>().volume = volume;
    }

    public void MuteOrUnMute()
    {
        if (PlayerPrefs.GetInt("SoundOff") == 1)
            Mute();
        else
            UnMute();
    }
}
