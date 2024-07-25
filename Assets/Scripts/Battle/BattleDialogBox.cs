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
    [SerializeField] GameObject dialogSelector;
    [SerializeField] GameObject itemSelector;
    [SerializeField] GameObject itemSecret;
    [SerializeField] List<TextMeshProUGUI> actionTexts;
    [SerializeField] List<TextMeshProUGUI> moveTexts;
    [SerializeField] List<TextMeshProUGUI> dialogTexts;
    [SerializeField] List<TextMeshProUGUI> itemTexts;

    // Referencias a los GameObjects de los botones
    [SerializeField] GameObject normalActionButton;
    [SerializeField] GameObject runActionButton;
    [SerializeField] GameObject talkActionButton;
    [SerializeField] GameObject itemActionButton;

    [SerializeField] Sprite normalStateSprite;
    [SerializeField] Sprite selectedStateSprite;
    [SerializeField] Sprite normalStateSpriteRun;
    [SerializeField] Sprite selectedStateSpriteRun;
    [SerializeField] Sprite normalStateSpriteTalk;
    [SerializeField] Sprite selectedStateSpriteTalk;
    [SerializeField] Sprite normalStateSpriteRunItem;
    [SerializeField] Sprite selectedStateSpriteRunItem;
    [SerializeField] Color talkColor = new Color32(0xA5, 0xF5, 0x95, 0xFF); // Color para "talk"
[SerializeField] Color itemColor = new Color32(0xE5, 0xE4, 0xA3, 0xFF); // Color para "item"
    


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
    }

    public void EnableDialogSelector(bool enable)
    {
        dialogSelector.SetActive(enable);
    }

    public void EnableItemSelector(bool enable)
    {
        itemSelector.SetActive(enable);
    }

    public void EnableItemSecret(bool enable)
    {
        itemSecret.SetActive(enable);
    }

public void UpdateActionSelection(int selectedAction)
    {
        // Actualiza el sprite y el color de cada botón basado en la acción seleccionada
        UpdateButton(normalActionButton, selectedAction == 0, normalStateSprite, selectedStateSprite);
        UpdateButton(runActionButton, selectedAction == 1, normalStateSpriteRun, selectedStateSpriteRun);
        UpdateButton(talkActionButton, selectedAction == 2, normalStateSpriteTalk, selectedStateSpriteTalk, talkColor);
        UpdateButton(itemActionButton, selectedAction == 3, normalStateSpriteRunItem, selectedStateSpriteRunItem, itemColor);
    }
    private void UpdateButton(GameObject buttonGameObject, bool isSelected, Sprite normalSprite, Sprite selectedSprite, Color? color = null)
    {
        Image buttonImage = buttonGameObject.GetComponent<Image>();
        buttonImage.sprite = isSelected ? selectedSprite : normalSprite;
        if (color.HasValue)
        {
            buttonImage.color = color.Value;
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

    public void UpdateDialogSelection(int selectedDialog)
    {
        for (int i = 0; i < dialogTexts.Count; ++i)
        {
            if (i == selectedDialog)
            
                dialogTexts[i].color = highlightedColor;
            
            else
            
                dialogTexts[i].color = Color.black;
            
        }
    }
    public void UpdateItemSelection(int selectedItem)
    {
        for (int i = 0; i < itemTexts.Count; ++i)
        {
            if (i == selectedItem)
            
                itemTexts[i].color = highlightedColor;
            
            else
            
                itemTexts[i].color = Color.black;
            
        }
    }

// public void UpdateItemDisplay(int itemIndex, int itemCount)
// {
    
//     if(itemIndex >= 0 && itemIndex < itemTexts.Count)
//     {
//         itemTexts[itemIndex].text = $"Item {itemIndex + 1}: {itemCount}";
//     }
// }
}
