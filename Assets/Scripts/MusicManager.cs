using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;

    [SerializeField] public float volume;

    private void Awake()
    {
        // Если экземпляра ещё нет, назначаем его и сохраняем между сценами
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект при загрузке новой сцены
        }
        else
        {
            // Если экземпляр уже существует, удаляем дублирующий объект
            Destroy(gameObject);
        }
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
        if (PlayerPrefs.GetInt("MusicOff") == 1)
            Mute();
        else
            UnMute();
    }
}
