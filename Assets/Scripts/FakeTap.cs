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
        // Получаем позицию мыши в экранных координатах
        Vector3 mousePosition = Input.mousePosition;
        // Конвертируем экранные координаты в мировые
        mousePosition.z = Camera.main.nearClipPlane; // Нужно установить значение Z для корректной конверсии
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Создаем объект MissTapPref в позиции курсора
        Instantiate(MissTapPref, worldPosition, Quaternion.identity);

        _gameManager.PlaySound(ClipTap);
        _gameManager.MissBubble();
    }
}
