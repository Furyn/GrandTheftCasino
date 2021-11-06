using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action_Voleur : ScriptableObject {
    protected bool done = false;

    public abstract void PerformAction(PlayerController player, bool reverse = false);
    //public abstract void PerformActionBackward(PlayerController player);

    public abstract bool CheckIfActionIsPossible(PlayerController player, bool reverse = false);
    //public abstract bool CheckIfBackwardActionIsPossible(PlayerController player);

    public bool IsDone() {
        return done;
    }

    public void Reset() {
        done = false;
    }
}