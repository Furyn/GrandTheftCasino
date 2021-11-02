using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Action[] actions;
    private int actualActionIndex;
    private bool canPerformAction;
    private bool isInAction;

    void Start()
    {
        isInAction = false;
        performAction(actions[0]);
    }

    void Update()
    {
        isInAction = !actions[actualActionIndex].getActionDone();
        if(!isInAction)
        {
            actualActionIndex++;
            performAction(actions[actualActionIndex]);
        }
    }

    void checkIfActionIsPossible(Action actionToPerform)
    {
        canPerformAction = actionToPerform.getCanBePerformed();
    }

    void performAction(Action actionToPerform)
    {
        if (!isInAction)
        {
            isInAction = true;
            if (canPerformAction) actionToPerform.performAction(player);
            else goBackToBeginning();
        }
    }

    void goBackToBeginning()
    {
        
    }
}
