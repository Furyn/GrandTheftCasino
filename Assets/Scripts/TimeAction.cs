using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAction : Action
{
    float timeToWait;
    override public void performAction(GameObject player)
    {

    }

    void setTime(float value)
    {
        timeToWait = value;
    }
}