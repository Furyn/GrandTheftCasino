using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    protected bool canBePerformed;
    protected bool actionDone;
    public abstract void PerformAction(GameObject player);

    public abstract void CheckIfActionIsPossible(GameObject player);

    public bool GetCanBePerformed()
    {
        return canBePerformed;
    }

    public bool GetActionDone()
    {
        return actionDone;
    }
}

