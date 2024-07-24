using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;
    [SerializeField] TMPro.TMP_Text dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;
    [SerializeField] List<TextMeshProUGUI> actionTexts;
    [SerializeField] List<TextMeshProUGUI> moveTexts;

    [SerializeField] Sprite normalStateSprite;
    [SerializeField] Sprite selectedStateSprite;
    [SerializeField] Sprite normalStateSpriteRun;
    [SerializeField] Sprite selectedStateSpriteRun;
    [SerializeField] Sprite normalStateSpriteTalk;
    [SerializeField] Sprite selectedStateSpriteTalk;
    [SerializeField] Sprite normalStateSpriteRunItem;
    [SerializeField] Sprite selectedStateSpriteRunItem;
    


    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enable)
    {
        dialogText.enabled = enable;
    }

    public void EnableActionSelector(bool enable)
    {
        actionSelector.SetActive(enable);
    }

    public void EnableMoveSelector(bool enable)
    {
        moveSelector.SetActive(enable);
        moveDetails.SetActive(enable);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; ++i)
        {
            Image parentImage = actionTexts[i].GetComponentInParent<Image>();
            if (i == selectedAction)
            
                parentImage.sprite = i == 0 ? selectedStateSprite : selectedStateSpriteRun;
            
            else
            
                parentImage.sprite = i == 0 ? normalStateSprite : normalStateSpriteRun;
            
        }
    }

    public void UpdateMoveSelection(int selectedMove)
    {
        for (int i = 0; i < moveTexts.Count; ++i)
        {
            if (i == selectedMove)
            
                moveTexts[i].color = highlightedColor;
            
            else
            
                moveTexts[i].color = Color.black;
            
        }
    }

    // public void SetMoveNames(List<MoveBase> moves)
    // {
    //     for (int i = 0; i < moveTexts.Count; ++i)
    //     {
    //         if (i < moves.Count)
    //         {
    //             moveTexts[i].text = moves[i].Name;
    //         }
    //         else
    //         {
    //             moveTexts[i].text = "-";
    //         }
    //     }
    // }

}
