using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, MoveSelection, TalkSelection, ItemSelection, DragonMove, Won, Lost }
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit dragonUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud dragonHud;
    // [SerializeField] GameObject playerPrefab;
    [SerializeField] BattleDialogBox dialogBox;
    // [SerializeField] Recolection recolection;

    BattleState state;
    int currentAction;
    int currentTalk;
    int currentMove;
    int currentItem;
    private int lastMoveIndex = 0;
    
    
    bool[] dialoguesUsed = new bool[2];

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        dragonUnit.Setup();
        yield return dialogBox.TypeDialog("Your dear dragon appeared!");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action:"));
        dialogBox.EnableActionSelector(true);
    }

    void MoveSelection()
    {
        state = BattleState.MoveSelection;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    void TalkSelection()
{
    state = BattleState.TalkSelection;
    dialogBox.EnableActionSelector(false);
    dialogBox.EnableDialogText(false);
    dialogBox.EnableDialogSelector(true);
}

    void ItemSelection()
{
    state = BattleState.ItemSelection;
    dialogBox.EnableActionSelector(false);
    dialogBox.EnableDialogText(false);
    dialogBox.EnableItemSelector(true);

    // int coliflorCount = recolection.ColiflorCount;
    // int remolachaCount = recolection.RemolachaCount;
    
    // dialogBox.UpdateItemDisplay(0, coliflorCount);
    // dialogBox.UpdateItemDisplay(1, remolachaCount);
}
    void EndBattle()
    {
        if(state == BattleState.Won)
        {
            StartCoroutine(dialogBox.TypeDialog("You won the battle!"));
        } else if(state == BattleState.Lost)
        {
            StartCoroutine(dialogBox.TypeDialog("You lost the battle!"));
        }
    }


    IEnumerator PlayerMove()
    {
        var move = playerUnit.Moves[currentMove];
        yield return dialogBox.TypeDialog($"You used {move.Base.Name}");

        playerUnit.ActivateParticle(move.Base.Type);
        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

            bool isDead = dragonUnit.TakeDamage(move);
            dragonHud.SetHP(dragonUnit.currentHP);
            yield return new WaitForSeconds(1f);
        if(isDead)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            state = BattleState.DragonMove;
            StartCoroutine(DragonMove());
        }
        
    }
IEnumerator PlayerRun()
{
    dialogBox.EnableActionSelector(false);
    yield return dialogBox.TypeDialog("You cannot run because the dragon is angry!");
    yield return new WaitForSeconds(2f);
    PlayerAction();
}
IEnumerator UsingItem()
{
    dialogBox.EnableItemSelector(false);
    dialogBox.EnableDialogText(true);

    yield return dialogBox.TypeDialog("You used the item!");

    playerUnit.PlayAttackAnimation();
    yield return new WaitForSeconds(1f);
    PlayerAction();
}

IEnumerator RespondToDialogSelection()
{
    dialogBox.EnableDialogSelector(false);
    dialogBox.EnableDialogText(true);

    // Marcar el diálogo actual como usado
    dialoguesUsed[currentTalk] = true;

    switch (currentTalk)
    {
        case 0:
            yield return dialogBox.TypeDialog("You didn't feed me what I wanted!");
            break;
        case 1:
            yield return dialogBox.TypeDialog("It was always on the other side of my nest...");
            break;
    }

    yield return new WaitForSeconds(2f);
    
    StartCoroutine(DragonMove());

    if (Array.TrueForAll(dialoguesUsed, used => used))
    {
        StartCoroutine(VerifyDialogsAndEnableItem());
    }
}
IEnumerator VerifyDialogsAndEnableItem()
{
    yield return new WaitUntil(() => Array.TrueForAll(dialoguesUsed, used => used));
    dialogBox.EnableItemSecret(true);
}

IEnumerator DragonMove()
{

    state = BattleState.DragonMove;
    lastMoveIndex = 1 - lastMoveIndex;
    var move = dragonUnit.Moves[lastMoveIndex];

    yield return dialogBox.TypeDialog($"Dragon used {move.Base.Name}");
    playerUnit.ActivateParticle(move.Base.Type);
    yield return new WaitForSeconds(1f);

    bool isDead = playerUnit.TakeDamage(move);
    playerHud.SetHP(playerUnit.currentHP);
    yield return new WaitForSeconds(1f);

    if(isDead)
    {
        state = BattleState.Lost;
        EndBattle();
    }
    else
    {
        state = BattleState.PlayerAction;
        PlayerAction();
    }
}

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.MoveSelection)
        {
            HandleMoveSelection();
        }
        else if (state == BattleState.TalkSelection)
    {
        HandleTalkSelection();
        }
        else if (state == BattleState.ItemSelection)
    {
        HandleItemSelection();
    }
    }
    void HandleActionSelection()
{
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        if (currentAction < 3)
            ++currentAction;
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        if (currentAction > 0)
            --currentAction;
    }
    else if (Input.GetKeyDown(KeyCode.RightArrow))
    {
        if (currentAction < 2)
            currentAction += 2;
    }
    else if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
        if (currentAction > 1)
            currentAction -= 2;
    }
    dialogBox.UpdateActionSelection(currentAction);

    if (Input.GetKeyDown(KeyCode.Space))
    {
        switch (currentAction)
        {
            case 0:
                // Fight
                MoveSelection();
                break;
            case 1:
                // Run
                StartCoroutine(PlayerRun());
                break;
            case 2:
                // Talk
                TalkSelection();
                break;
            case 3:
                // Item
                ItemSelection();
                break;
        }
    }
}

void HandleMoveSelection(){
    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
        if (currentMove < 3)
            ++currentMove;
    }
    else if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
        if (currentMove > 0)
            --currentMove;
    }
    else if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        if (currentMove < 2)
            currentMove += 2;
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        if (currentMove > 1)
            currentMove -= 2;
    }
    dialogBox.UpdateMoveSelection(currentMove);

    if (Input.GetKeyDown(KeyCode.Space))
    {
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableDialogText(true);
        StartCoroutine(PlayerMove());
}
}
void HandleTalkSelection()
{
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        if (currentTalk < 1)
            ++currentTalk;
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        if (currentTalk > 0)
            --currentTalk;
    }

    dialogBox.UpdateDialogSelection(currentTalk);
    
    if (Input.GetKeyDown(KeyCode.Space))
    {
        StartCoroutine(RespondToDialogSelection());
    }
}

void HandleItemSelection()
{
    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
    {
        if (currentItem < 2)
            ++currentItem;
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
    {
        if (currentItem > 0)
            --currentItem;
    }

    dialogBox.UpdateItemSelection(currentItem);
        if (Input.GetKeyDown(KeyCode.Space))
    {
        StartCoroutine(UsingItem());
    }
}


}