using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalAction : Action
{
    GameObject condition;
    override public void performAction(GameObject player)
    {

    }

    void setCondition(GameObject value)
    {
        condition = value;
    }
}