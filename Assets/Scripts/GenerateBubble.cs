using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBubble : MonoBehaviour
{
    public GameObject BubblePref;
    public GameObject BubblePref2;

    public float SpawnDistance = 2f; // �� ����� ���������� �� ��������� ������ �������� �������
    public float SpawnInterval = 2f; // �������� ����� ������� ��������
    public float ChanceFake = 10;

    private Camera mainCamera;
    private float _screenWidth;
    private float _screenHeight;

    void Start()
    {
        // �������� ������
        mainCamera = Camera.main;

        // ������������ ������ � ������ ������ � ������� �����������
        _screenHeight = 2f * mainCamera.orthographicSize;
        _screenWidth = _screenHeight * mainCamera.aspect;

        // �������� ����� �������� � ��������� ����������
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnInterval);
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        // ���������� ��������� ����� �� ��������� ������
        Vector2 spawnPosition = GetSpawnPosition();
        float range = Random.Range(0, 100);
        if (range > ChanceFake)
        {
            GameObject.Instantiate(BubblePref, spawnPosition, Quaternion.identity);
        }
        else
        {
            GameObject.Instantiate(BubblePref2, spawnPosition, Quaternion.identity);
        }
    }

    Vector2 GetSpawnPosition()
    {
        // �������� ��������� ������� ��� ������ (����, ���, �����, ������)
        int side = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // ����
                spawnPosition = new Vector2(Random.Range(-_screenWidth / 2, _screenWidth / 2), mainCamera.transform.position.y + _screenHeight / 2 + SpawnDistance);
                break;
            case 1: // ���
                spawnPosition = new Vector2(Random.Range(-_screenWidth / 2, _screenWidth / 2), mainCamera.transform.position.y - _screenHeight / 2 - SpawnDistance);
                break;
            case 2: // �����
                spawnPosition = new Vector2(mainCamera.transform.position.x - _screenWidth / 2 - SpawnDistance, Random.Range(-_screenHeight / 2, _screenHeight / 2));
                break;
            case 3: // ������
                spawnPosition = new Vector2(mainCamera.transform.position.x + _screenWidth / 2 + SpawnDistance, Random.Range(-_screenHeight / 2, _screenHeight / 2));
                break;
        }

        // ��������� �������� �� ������� ������
        spawnPosition += (Vector2)mainCamera.transform.position;

        return spawnPosition;
    }
}
