using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour {
    [SerializeField] PlayerController player = null;
    [SerializeField] List<Action_Voleur> actions = null;
    [SerializeField] List<Action_Voleur> actionsDone = null;
    private int actualActionIndex;
    //private bool canPerformAction;
    private bool isInAction;
    public bool reverse = false;
    public bool inWork = false;

    void Start() {
        isInAction = false;
    }

    void Update() {
        if (!inWork || actions.Count == 0) { return; }

        isInAction = !actions[actualActionIndex].GetActionDone();
        if (!isInAction) {
            actions[actualActionIndex].Reset();
            actualActionIndex++;
            if (actualActionIndex < actions.Count) {
                PerformAction(actions[actualActionIndex]);
            } else {
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
        PerformAction(actions[0]);
        inWork = true;
    }

    public void ResetManager() {
        actions.Clear();
        reverse = false;
        isInAction = false;
        inWork = false;
        actualActionIndex = 0;
        actionsDone.Clear();
    }

    //void CheckIfActionIsPossible(Action actionToPerform) {
    //    canPerformAction = actionToPerform.GetCanBePerformed();
    //}

    void PerformAction(Action_Voleur actionToPerform) {
        if (!isInAction) {
            isInAction = true;
            actionToPerform.CheckIfActionIsPossible(player, reverse);
            if (actionToPerform.GetCanBePerformed()) {
                actionToPerform.PerformAction(player, reverse);
                // Conditional
                if (actionToPerform as ConditionalAction != null) {
                    if (actionToPerform.GetActionDone()) {
                        DowngradeAction();
                    }
                } else {
                    if (actionToPerform as TimeAction == null) {
                        actionsDone.Add(actionToPerform);
                    }
                }
            }
            else GoBackToBeginning();
        }
    }

    void DowngradeAction() {
        actualActionIndex--;
        isInAction = false;
        PerformAction(actions[actualActionIndex]);
    }

    void GoBackToBeginning() {
        if (actionsDone.Count == 0) { return; }
        if (reverse) { return; }
        actions[actualActionIndex].Reset();
        player.SetRotation(player.orientation.Inverse());
        actionsDone.Reverse();
        actualActionIndex = 0;
        isInAction = false;
        reverse = true;
        actions = new List<Action_Voleur>(actionsDone);
        if (actions.Count > 0) {
            PerformAction(actions[actualActionIndex]);
        }
    }
}
