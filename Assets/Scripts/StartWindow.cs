using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartWindow : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] public GameObject LoseWindow;
    [SerializeField] public TextMeshProUGUI TextPoint;
    [SerializeField] private AudioClip ClipTap;
    void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
        _gameManager.GamePause();
        if (PlayerPrefs.GetInt("Points") != 0)
        {
            CallLoseWindow();
        }
    }

    public void GameStart()
    {
        _gameManager.PlaySound(ClipTap);
        _gameManager.GameUnPause();
        gameObject.SetActive(false);
    }

    public void CallLoseWindow()
    {
        LoseWindow.SetActive(true);
        TextPoint.text = PlayerPrefs.GetInt("Points").ToString();
    }
}
