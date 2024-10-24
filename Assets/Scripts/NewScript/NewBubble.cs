using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBubble : MonoBehaviour
{
    [SerializeField] public float Duration = 2f;
    [SerializeField] public float Speed = 0.5f;
    [SerializeField] public float DeltaDuration = 0.1f;
    [SerializeField] public float DeltaSpeed = 0.1f;
    [SerializeField] public Vector2 Point = Vector2.zero;
    [SerializeField] public Color StartColor = new Color(1f, 1f, 1f, 1f); // Белый (1, 1, 1, 1)
    [SerializeField] public Color EndColor = new Color(1f, 0f, 0f, 220f / 255f); // Красный (1, 0, 0, 200/255)
    [SerializeField] private GameObject TapBubbleDeadPref;
    [SerializeField] private GameObject NoTapBubbleDeadPref;
    [SerializeField] private AudioClip ClipTap;
    [SerializeField] private AudioClip ClipNoTap;
    private float _timeElapsed = 0f;
    private Image _image;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
        _image = GetComponent<Image>();
        Duration = Random.Range(Duration - DeltaDuration, Duration + DeltaDuration);
        Speed = Random.Range(Speed - DeltaSpeed, Speed + DeltaSpeed);
    }
    void Update()
    {
        if (_timeElapsed < Duration)
        {
            _timeElapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, Point, Speed * Time.deltaTime);
            _image.color = Color.Lerp(StartColor, EndColor, _timeElapsed / Duration);
        }

        else
        {
            GameObject.Instantiate(NoTapBubbleDeadPref, transform.position, Quaternion.identity);
            _gameManager.LoseBubble();
            _gameManager.PlaySound(ClipNoTap);
            GameObject.Destroy(gameObject);
        }
    }


    public void Tap()
    {
        if (_gameManager.GameIsPause())
            return;
        GameObject.Instantiate(TapBubbleDeadPref, transform.position, Quaternion.identity);
        _gameManager.TapBubble();
        _gameManager.PlaySound(ClipTap);
        Destroy(gameObject);
    }
}
