using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private int BubblePoint = 10;
    private int _curPoint;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
        _gameManager.OnPlayerTapBubble += BuffPoint;
    }

    private void BuffPoint()
    {
        _curPoint += BubblePoint;
        _gameManager.ChangePoint(_curPoint);
    }

    public int GetPoints()
    {
        return _curPoint;
    }

    private void OnDisable()
    {
        _gameManager.OnPlayerTapBubble -= BuffPoint;
    }

}
