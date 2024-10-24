using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [HideInInspector] public MusicManager MusicManagerO;
    [SerializeField] private Sprite SpriteOn;
    [SerializeField] private Sprite SpriteOff;
    private int _isSoundPlaed;

    private void Start()
    {
        MusicManagerO = FindAnyObjectByType<MusicManager>();
        _isSoundPlaed = PlayerPrefs.GetInt("MusicOff");
        if (_isSoundPlaed == 1)
            gameObject.GetComponent<Image>().sprite = SpriteOff;
        else
            gameObject.GetComponent<Image>().sprite = SpriteOn;
        MusicManagerO.MuteOrUnMute();

    }

    public void ChangePlaySound()
    {
        _isSoundPlaed = PlayerPrefs.GetInt("MusicOff");
        if (_isSoundPlaed == 0)
        {
            gameObject.GetComponent<Image>().sprite = SpriteOff;
            PlayerPrefs.SetInt("MusicOff", 1);
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = SpriteOn;
            PlayerPrefs.SetInt("MusicOff", 0);
        }
        if (MusicManagerO == null) 
        {
            MusicManagerO = FindAnyObjectByType<MusicManager>();
        }
        MusicManagerO.MuteOrUnMute();

    }
}
