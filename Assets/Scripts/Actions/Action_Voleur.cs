using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action_Voleur : ScriptableObject {
    protected bool canBePerformed;
    protected bool actionDone;

    public abstract void PerformAction(PlayerController player, bool reverse = false);
    public abstract void PerformActionBackward(PlayerController player);

    public abstract void CheckIfActionIsPossible(PlayerController player, bool reverse = false);
    public abstract void CheckIfBackwardActionIsPossible(PlayerController player);

    public bool GetCanBePerformed() {
        return canBePerformed;
    }

    public bool GetActionDone() {
        return actionDone;
    }

    public void Reset() {
        canBePerformed = false;
        actionDone = false;
    }
}