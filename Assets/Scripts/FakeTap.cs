using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTap : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameObject MissTapPref;
    [SerializeField] private AudioClip ClipTap;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    public void Tap()
    {
        if (_gameManager.GameIsPause())
            return;
        // �������� ������� ���� � �������� �����������
        Vector3 mousePosition = Input.mousePosition;
        // ������������ �������� ���������� � �������
        mousePosition.z = Camera.main.nearClipPlane; // ����� ���������� �������� Z ��� ���������� ���������
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ������� ������ MissTapPref � ������� �������
        Instantiate(MissTapPref, worldPosition, Quaternion.identity);

        _gameManager.PlaySound(ClipTap);
        _gameManager.MissBubble();
    }
}
