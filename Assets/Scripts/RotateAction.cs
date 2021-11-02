using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAction : Action
{
    float rotateValue;
    override public void performAction(GameObject player)
    {

    }

    void setRotateValue(float value)
    {
        rotateValue = value;
    }
}