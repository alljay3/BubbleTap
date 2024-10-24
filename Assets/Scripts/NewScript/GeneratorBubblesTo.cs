using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBubblesTo : MonoBehaviour
{
    public GameObject BubblePref;
    public GameObject BubblePref2;
    public Canvas MyCanvas;

    public float SpawnDistance = 2f; // �� ����� ���������� �� ��������� ������ �������� �������
    public float SpawnInterval = 2f; // �������� ����� ������� ��������
    public float ChanceFake = 10;

    private RectTransform canvasRect;

    void Start()
    {
        // �������� RectTransform �������
        canvasRect = MyCanvas.GetComponent<RectTransform>();

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
        // ���������� ��������� ����� �� ��������� �������
        Vector2 spawnPosition = GetSpawnPosition();

        // ������� ������
        float range = Random.Range(0, 100);
        GameObject obj;
        if (range > ChanceFake)
        {
            obj = GameObject.Instantiate(BubblePref, MyCanvas.transform);
        }
        else
        {
            obj = GameObject.Instantiate(BubblePref2, MyCanvas.transform);
        }

        // ����������� ������� ������� �� �������
        RectTransform objRect = obj.GetComponent<RectTransform>();
        objRect.anchoredPosition = spawnPosition;
    }

    Vector2 GetSpawnPosition()
    {
        // ������������ ������� �������
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        // �������� ��������� ������� ��� ������ (����, ���, �����, ������)
        int side = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // ����
                spawnPosition = new Vector2(Random.Range(-canvasWidth / 2, canvasWidth / 2), canvasHeight / 2 + SpawnDistance);
                break;
            case 1: // ���
                spawnPosition = new Vector2(Random.Range(-canvasWidth / 2, canvasWidth / 2), -canvasHeight / 2 - SpawnDistance);
                break;
            case 2: // �����
                spawnPosition = new Vector2(-canvasWidth / 2 - SpawnDistance, Random.Range(-canvasHeight / 2, canvasHeight / 2));
                break;
            case 3: // ������
                spawnPosition = new Vector2(canvasWidth / 2 + SpawnDistance, Random.Range(-canvasHeight / 2, canvasHeight / 2));
                break;
        }

        return spawnPosition;
    }
}
