// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, MoveSelection, DragonMove, Busy, PlayerRun, PlayerTalk, PlayerUseItem }
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;
    // public event Action OnBattleOver;

    BattleState state;
    int currentAction;
    int currentMove;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        //playerHud.SetHUD(playerUnit);
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

IEnumerator RunAttempt()
{
    state = BattleState.PlayerRun;
    dialogBox.EnableActionSelector(false);
    dialogBox.EnableDialogText(true);
    yield return dialogBox.TypeDialog("You cannot run because the dragon is angry!");
    yield return new WaitForSeconds(2);
    PlayerAction();
}

    IEnumerator PlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.Moves[currentMove];
        yield return dialogBox.TypeDialog($"You used {move.Base.Name}");
        

        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        // bool isFainted = playerUnit.TakeDamage(move, playerUnit);
        // enemyHud.UpdateHP();

        // if (isFainted)
        // {
        //     yield return dialogBox.TypeDialog("The dragon fainted");
        // }
        // else
        // {
        //    // StartCoroutine(DragonMove());
        // }

    }

    // IEnumerator DragonMove()
    // {
    //     state = BattleState.DragonMove;

    //     var move = playerUnit.Moves.GetRandomMove();
    //     yield return dialogBox.TypeDialog($"Dragon used {move.Base.Name}");
    //     yield return new WaitForSeconds(1f);

    //     bool isFainted = playerUnit.TakeDamage(move, playerUnit);
    //     playerHud.UpdateHP();

    //     if (isFainted)
    //     {
    //         yield return dialogBox.TypeDialog("You are fainted");
    //     }
    //     else
    //     {
    //         PlayerAction();
    //     }
    // }

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

    if (Input.GetKeyDown(KeyCode.Z))
    {
        switch (currentAction)
        {
            case 0:
                // Fight
                MoveSelection();
                break;
            case 1:
                // Talk
                break;
            case 2:
                // Run
                StartCoroutine(RunAttempt());
                break;
            case 3:
                // Item
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

    if (Input.GetKeyDown(KeyCode.Z))
    {
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableDialogText(true);
        StartCoroutine(PlayerMove());
}
}
}
