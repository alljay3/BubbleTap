using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [HideInInspector] public SoundManager SoundManagerO;
    [SerializeField] private Sprite SpriteOn;
    [SerializeField] private Sprite SpriteOff;
    private int _isSoundPlaed;

    private void Start()
    {
        SoundManagerO = FindAnyObjectByType<SoundManager>();
        _isSoundPlaed = PlayerPrefs.GetInt("SoundOff");
        if (_isSoundPlaed == 1)
            gameObject.GetComponent<Image>().sprite = SpriteOff;
        else
            gameObject.GetComponent<Image>().sprite = SpriteOn;
        SoundManagerO.MuteOrUnMute();

    }

    public void ChangePlaySound()
    {
        _isSoundPlaed = PlayerPrefs.GetInt("SoundOff");
        if (_isSoundPlaed == 0)
        {
            gameObject.GetComponent<Image>().sprite = SpriteOff;
            PlayerPrefs.SetInt("SoundOff", 1);
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = SpriteOn;
            PlayerPrefs.SetInt("SoundOff", 0);
        }
        SoundManagerO.MuteOrUnMute();

    }


}
