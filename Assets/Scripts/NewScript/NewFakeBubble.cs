using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFakeBubble : MonoBehaviour
{
    [SerializeField] public float Duration = 2f;
    [SerializeField] public float Speed = 0.5f;
    [SerializeField] public float DeltaDuration = 0.1f;
    [SerializeField] public float DeltaSpeed = 0.1f;
    [SerializeField] public Vector2 Point = Vector2.zero;
    [SerializeField] public Color StartColor = new Color(1f, 1f, 1f, 1f); // Белый (1, 1, 1, 1)
    [SerializeField] public Color EndColor = new Color(1f, 0f, 0f, 220f / 255f); // Красный (1, 0, 0, 200/255)
    [SerializeField] private GameObject TapFakeBubbleDeadPref;
    [SerializeField] private GameObject NoTapFakeBubbleDeadPref;
    [SerializeField] private AudioClip ClipTap;
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
            Instantiate(NoTapFakeBubbleDeadPref, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Tap()
    {
        if (_gameManager.GameIsPause())
            return;
        Instantiate(TapFakeBubbleDeadPref, transform.position, Quaternion.identity);
        _gameManager.FakeBubble();
        _gameManager.PlaySound(ClipTap);
        Destroy(gameObject);

    }
}
