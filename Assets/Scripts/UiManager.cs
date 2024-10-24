using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Slider HpBar;
    [SerializeField] private TextMeshProUGUI Point;

    public void ChangeHp(int hp)
    {
        HpBar.value = hp;
    }

    public void ChangePoint(int point)
    {
        Point.text = point.ToString();
    }

}
