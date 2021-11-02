using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelAction : Action
{
    Action[] actionsToPerform;
    int actualActionsIndex;
    override public void performAction(GameObject player)
    {

    }

    public void setActionsToPerform(Action[] actions)
    {
        actionsToPerform = actions;
    }

    public void setActualIndex(int index)
    {
        actualActionsIndex = index;
    }
}