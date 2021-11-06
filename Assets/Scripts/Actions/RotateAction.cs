using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RotationAction", menuName = "ScriptableObjects/Actions/RotationAction", order = 1)]
public class RotateAction : Action_Voleur {
    public Tools.Directions rotation;
    public bool addPlayerDirection;

    override public void PerformAction(PlayerController player, bool reverse = false) {
        if (reverse) {
            PerformActionBackward(player);
            return;
        }

        Tools.Directions tempRot = rotation;
        if (addPlayerDirection) {
            tempRot = rotation.Add(player.orientation);
        }
        player.SetRotation(tempRot);
        done = true;
    }

    public void PerformActionBackward(PlayerController player) {
        if (addPlayerDirection) {
            PerformAction(player);
        } else {
            Tools.Directions tempRot = rotation;
            rotation = rotation.Inverse();
            PerformAction(player);
            rotation = tempRot;
        }
    }

    override public bool CheckIfActionIsPossible(PlayerController player, bool reverse = false) {
        return true;
    }
}