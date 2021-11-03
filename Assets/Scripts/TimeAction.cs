using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAction : Action
{
    public float timeToWait;
    override public void PerformAction(GameObject player)
    {
        StartCoroutine(WaitCoroutine());
    }

    void setTime(float value)
    {
        timeToWait = value;
    }

    IEnumerator WaitCoroutine()
    { 
        yield return new WaitForSeconds(timeToWait);
    }

    override public void CheckIfActionIsPossible(GameObject player)
    {
        canBePerformed = true;
    }
}
