using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHud : MonoBehaviour
{
    [SerializeField] HPBar hpBar;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;

    public void SetHUD()
    {
        nameText.text = "Dragon Boss";
        levelText.text = "Lvl 30";
        hpBar.SetHP((float) 45 / 50);
    }
}
