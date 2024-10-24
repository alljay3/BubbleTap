using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBubblesTo : MonoBehaviour
{
    public GameObject BubblePref;
    public GameObject BubblePref2;
    public Canvas MyCanvas;

    public float SpawnDistance = 2f; // На каком расстоянии за пределами камеры спаунить объекты
    public float SpawnInterval = 2f; // Интервал между спауном объектов
    public float ChanceFake = 10;

    private RectTransform canvasRect;

    void Start()
    {
        // Получаем RectTransform канваса
        canvasRect = MyCanvas.GetComponent<RectTransform>();

        // Начинаем спаун объектов с указанным интервалом
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
        // Определяем случайное место за пределами канваса
        Vector2 spawnPosition = GetSpawnPosition();

        // Создаем объект
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

        // Присваиваем объекту позицию на канвасе
        RectTransform objRect = obj.GetComponent<RectTransform>();
        objRect.anchoredPosition = spawnPosition;
    }

    Vector2 GetSpawnPosition()
    {
        // Рассчитываем границы канваса
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        // Выбираем случайную сторону для спауна (верх, низ, слева, справа)
        int side = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // Верх
                spawnPosition = new Vector2(Random.Range(-canvasWidth / 2, canvasWidth / 2), canvasHeight / 2 + SpawnDistance);
                break;
            case 1: // Низ
                spawnPosition = new Vector2(Random.Range(-canvasWidth / 2, canvasWidth / 2), -canvasHeight / 2 - SpawnDistance);
                break;
            case 2: // Слева
                spawnPosition = new Vector2(-canvasWidth / 2 - SpawnDistance, Random.Range(-canvasHeight / 2, canvasHeight / 2));
                break;
            case 3: // Справа
                spawnPosition = new Vector2(canvasWidth / 2 + SpawnDistance, Random.Range(-canvasHeight / 2, canvasHeight / 2));
                break;
        }

        return spawnPosition;
    }
}
