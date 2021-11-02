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
        PerformAction(actions[0]);
    }

    void Update()
    {
        isInAction = !actions[actualActionIndex].GetActionDone();
        if(!isInAction)
        {
            actualActionIndex++;
            
        }
        PerformAction(actions[actualActionIndex]);
    }

    void CheckIfActionIsPossible(Action actionToPerform)
    {
        canPerformAction = actionToPerform.GetCanBePerformed();
    }

    void PerformAction(Action actionToPerform)
    {
        if (!isInAction)
        {
            isInAction = true;
            if (canPerformAction) actionToPerform.PerformAction(player);
            else GoBackToBeginning();
        }
    }

    void GoBackToBeginning()
    {
        
    }
}
