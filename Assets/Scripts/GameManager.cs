using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using YG;
using YG.Utils.LB;

public class GameManager : MonoBehaviour
{
    [SerializeField] float TimeSpeedUp = 0.01f;
    [SerializeField] float DelayTimeSpeedUp = 0.1f;
    [HideInInspector] public HealthManager HealthManagerO;
    [HideInInspector] public UiManager UiManagerO;
    [HideInInspector] public PointManager PointManagerO;
    [HideInInspector] public StartWindow StartWindowO;
    [HideInInspector] public MusicManager MusicManagerO;
    [HideInInspector] public SoundManager SoundManagerO;
    public event Action OnPlayerTapBubble;
    public event Action OnPlayerTapFakeBubble;
    public event Action OnPlayerTapMiss;
    public event Action OnPlayerLoseBubble;
    private float _timeScale = 1;
    bool _gamePause = false;
    public void Start()
    {
        PointManagerO = FindAnyObjectByType<PointManager>();
        HealthManagerO = FindAnyObjectByType<HealthManager>();
        UiManagerO = FindAnyObjectByType<UiManager>();
        StartWindowO  = FindAnyObjectByType<StartWindow>();
        MusicManagerO = FindAnyObjectByType<MusicManager>();
        SoundManagerO = FindAnyObjectByType<SoundManager>();
        StartCoroutine(IETimeSpeedUp());
    }


    public void Update()
    {
        if (_gamePause == false)
            Time.timeScale = _timeScale;
        else
            Time.timeScale = 0;
    }

    public void ChangeHp(int curHp)
    {
        UiManagerO.ChangeHp(curHp);
        if (curHp == 0)
        {
            GameEnd();
        }    
    }

    public void ChangePoint(int point)
    {
        UiManagerO.ChangePoint(point);
    }


    public void GameEnd()
    {
        PlayerPrefs.SetInt("Points", GetPoints());
        GetLeaderboard();
        if (MusicManagerO == null)
        {
            MusicManagerO = FindAnyObjectByType<MusicManager>();
        }
        DontDestroyOnLoad(MusicManagerO.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TapBubble()
    {
        OnPlayerTapBubble.Invoke();
    }

    public void LoseBubble()
    {
        OnPlayerLoseBubble.Invoke(); 
    }

    public void MissBubble()
    {
        OnPlayerTapMiss.Invoke();   
    }

    public void FakeBubble()
    {
        OnPlayerTapFakeBubble.Invoke(); 
    }

    public void GamePause()
    {
        _gamePause = true;
    }


    public void GameUnPause()
    {
        _gamePause = false;
    }

    public bool GameIsPause()
    {
        return _gamePause;
    }


    public void PlaySound(AudioClip audioClip)
    {
        SoundManagerO.PlaySound(audioClip);
    }

    public int GetPoints()
    {
        return PointManagerO.GetPoints();
    }
    IEnumerator IETimeSpeedUp()
    {
        while (true)
        {
            if (GameIsPause())
            {
                yield return null;
                continue;
            }
            yield return new WaitForSeconds(DelayTimeSpeedUp);
            _timeScale += TimeSpeedUp;
        }
    }

    private void OnEnable()
    {
        YandexGame.onGetLeaderboard += OnGetLeaderboard;
    }
    private void OnDisable()
    {
        YandexGame.onGetLeaderboard -= OnGetLeaderboard;
    }

    public void GetLeaderboard()
    {
        YandexGame.GetLeaderboard("Points", 10, 3, 3, "nonePhoto");
    }

    private void OnGetLeaderboard(LBData lb)
    {
        // Сверяем имя лидерборда
        if (lb.technoName == "Points")
        {
            if (lb.thisPlayer.score < PlayerPrefs.GetInt("Points"))
                YandexGame.NewLeaderboardScores("Points", PlayerPrefs.GetInt("Points"));
        }
    }

}
