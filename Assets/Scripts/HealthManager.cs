using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int MaxHp;
    [SerializeField] private int BubbleTap;
    [SerializeField] private int MissTap;
    [SerializeField] private int FakeBubbleTap;
    [SerializeField] private int LoseBubble;
    [SerializeField] private int PermanentDamage = 1;
    [SerializeField] private float DelayPermanentDamage = 1f;
    private int _curHp;
    private GameManager _gameManager;
    void Start()
    {
        _curHp = MaxHp;
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
        _gameManager.OnPlayerTapBubble += HealthBubble;
        _gameManager.OnPlayerTapFakeBubble += DamageFakeBubble;
        _gameManager.OnPlayerTapMiss += DamageMiss;
        _gameManager.OnPlayerLoseBubble += DamageBubble;
        StartCoroutine(IEPermanentDamage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        _gameManager.OnPlayerTapBubble -= HealthBubble;
        _gameManager.OnPlayerTapFakeBubble -= DamageFakeBubble;
        _gameManager.OnPlayerTapMiss -= DamageMiss;
        _gameManager.OnPlayerLoseBubble -= DamageBubble;
    }

    private void HealthBubble()
    {
        Health(BubbleTap);
    }

    private void DamageBubble()
    {
        Damage(LoseBubble);
    }

    private void DamageFakeBubble()
    {
        Damage(FakeBubbleTap);
    }

    private void DamageMiss()
    {
        Damage(MissTap);
    }

    private void Damage(int damage)
    {
        _curHp -= damage;
        if (_curHp < 0)
            _curHp = 0;
        _gameManager.ChangeHp(_curHp);
    }

    private void Health(int heal)
    {
        _curHp += heal;
        if (_curHp > MaxHp)
            _curHp = MaxHp;
        _gameManager.ChangeHp(_curHp);
    }

    IEnumerator IEPermanentDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(DelayPermanentDamage);
            Damage(PermanentDamage);
        }
    }
}
