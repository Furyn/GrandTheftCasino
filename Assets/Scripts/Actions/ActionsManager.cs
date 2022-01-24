using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour {
    [SerializeField] PlayerController player = null;
    [SerializeField] List<Action_Voleur> actions = null;
    public List<Action_Voleur> actionsDone = null;
    private int actualActionIndex;
    //private bool canPerformAction;
    public bool reverse = false;
    public bool inWork = false;

    void Update() {
        if (!inWork || actions.Count == 0) { return; }

        if (actions[actualActionIndex].IsDone()) {
            actions[actualActionIndex].Reset();
            actualActionIndex++;
            if (actualActionIndex < actions.Count) {
                    PerformAction(actions[actualActionIndex]);
            } else {
                player.WorkDone();
                ResetManager();
            }
        }
    }

    public void ExecuteActions(List<Action_Voleur> actionsToDo) {
        ResetManager();
        actions = new List<Action_Voleur>(actionsToDo);
        if (actions.Count == 0) { return; }
        StartManager();
    }

    public void StartManager() {
        actionsDone.Clear();
        PerformAction(actions[actualActionIndex]);
        inWork = true;
    }

    public void ResetManager() {
        for (int i = 0; i < actions.Count; i++) {
            actions[i].Reset();
        }
        actions.Clear();
        reverse = false;
        inWork = false;
        actualActionIndex = 0;
    }

    //void CheckIfActionIsPossible(Action actionToPerform) {
    //    canPerformAction = actionToPerform.GetCanBePerformed();
    //}

    void PerformAction(Action_Voleur actionToPerform) {
        if (actionToPerform.CheckIfActionIsPossible(player, reverse)) {
            Debug.Log("Performing : " + actionToPerform.name + (reverse ? " reversed " : " "));
            actionToPerform.PerformAction(player, reverse);
            // Conditional
            if (actionToPerform as ConditionalAction != null) {
                if (actionToPerform.IsDone()) {
                    DowngradeAction();
                }
            } else {
                if (actionToPerform as TimeAction == null) {
                    actionsDone.Add(actionToPerform);
                }
            }
            //Debug.Log("ALORS PEUT ETRE");
        } else {
            Debug.LogWarning("Impossibru");
            GoBackToBeginning();
        }
    }

    void DowngradeAction() {
        actualActionIndex--;
        PerformAction(actions[actualActionIndex]);
    }

    public void GoBackToBeginning() {
        //if (actionsDone.Count == 0) { return; }
        //if (reverse) { return; }
        //Debug.LogWarning("Reversing");
        //ResetManager();
        //player.SetRotation(player.orientation.Inverse());
        //actionsDone.Reverse();
        //inWork = true;
        //reverse = true;
        //actions = new List<Action_Voleur>(actionsDone);
        //if (actions.Count > 0) {
        //    PerformAction(actions[actualActionIndex]);
        //} else {
        //    player.WorkDone();
        //    ResetManager();
        //}
        player.GoBackToOrigin();
    }
}
