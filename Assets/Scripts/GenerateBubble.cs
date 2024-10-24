using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBubble : MonoBehaviour
{
    public GameObject BubblePref;
    public GameObject BubblePref2;

    public float SpawnDistance = 2f; // На каком расстоянии за пределами камеры спаунить объекты
    public float SpawnInterval = 2f; // Интервал между спауном объектов
    public float ChanceFake = 10;

    private Camera mainCamera;
    private float _screenWidth;
    private float _screenHeight;

    void Start()
    {
        // Получаем камеру
        mainCamera = Camera.main;

        // Рассчитываем ширину и высоту экрана в мировых координатах
        _screenHeight = 2f * mainCamera.orthographicSize;
        _screenWidth = _screenHeight * mainCamera.aspect;

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
        // Определяем случайное место за пределами камеры
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
        // Выбираем случайную сторону для спауна (верх, низ, слева, справа)
        int side = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // Верх
                spawnPosition = new Vector2(Random.Range(-_screenWidth / 2, _screenWidth / 2), mainCamera.transform.position.y + _screenHeight / 2 + SpawnDistance);
                break;
            case 1: // Низ
                spawnPosition = new Vector2(Random.Range(-_screenWidth / 2, _screenWidth / 2), mainCamera.transform.position.y - _screenHeight / 2 - SpawnDistance);
                break;
            case 2: // Слева
                spawnPosition = new Vector2(mainCamera.transform.position.x - _screenWidth / 2 - SpawnDistance, Random.Range(-_screenHeight / 2, _screenHeight / 2));
                break;
            case 3: // Справа
                spawnPosition = new Vector2(mainCamera.transform.position.x + _screenWidth / 2 + SpawnDistance, Random.Range(-_screenHeight / 2, _screenHeight / 2));
                break;
        }

        // Добавляем смещение от позиции камеры
        spawnPosition += (Vector2)mainCamera.transform.position;

        return spawnPosition;
    }
}
