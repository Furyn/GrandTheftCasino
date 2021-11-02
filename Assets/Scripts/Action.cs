using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    protected bool canBePerformed;
    protected bool actionDone;
    public abstract void performAction(GameObject player);

    public bool getCanBePerformed()
    {
        return canBePerformed;
    }

    public bool getActionDone()
    {
        return actionDone;
    }
}

