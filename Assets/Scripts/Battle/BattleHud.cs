using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHud : MonoBehaviour
{
    [SerializeField] HPBar hpBar;
    [SerializeField] TMP_Text nameText;

    public void SetHUD(BattleUnit player)
    {
    //    _player = player;
        nameText.text = "Player";
        hpBar.SetHP((float) 45 / 50);
    }
    // public void UpdateHP()
    //     {
    //     hpBar.SetHP((float) _player.HP / _player.MaxHP);
    // }
}
