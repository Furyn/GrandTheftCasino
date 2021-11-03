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
        if (actions.Length > 0) {
            isInAction = !actions[actualActionIndex].GetActionDone();
            if (!isInAction)
            {
                actualActionIndex++;

            }
            CheckIfActionIsPossible(actions[actualActionIndex]);
            if (canPerformAction)
                PerformAction(actions[actualActionIndex]);
        }
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
        List<Action> actionsList = new List<Action>();
        for (int i = actualActionIndex; i >= 0; i--)
        {
            if (actions[i] is MoveAction)
            {
                actionsList.Add(actions[i]);
            }
            
        }
        Action[] reverseMovesAction = new Action[actionsList.Count];

        for (int y = 0; y < actionsList.Count; y++)
        {
            reverseMovesAction[y] = actionsList[y];
        }
        actions = reverseMovesAction;
    }
}
