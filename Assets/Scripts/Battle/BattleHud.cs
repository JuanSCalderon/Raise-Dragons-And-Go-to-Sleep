using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Slider hpBar;
    [SerializeField] TMP_Text nameText;

    public void SetHUD(BattleUnit unit)
    {
        nameText.text = unit.unitName;
        hpBar.maxValue = unit.maxHP;
        hpBar.value = unit.currentHP;
    }
public void SetHP(int hp)
{
    hpBar.value = hp;
}
}
