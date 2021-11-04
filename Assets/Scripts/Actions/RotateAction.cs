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
    }

    override public void PerformActionBackward(PlayerController player) {
        if (addPlayerDirection) {
            PerformAction(player);
        } else {
            Tools.Directions tempRot = rotation;
            rotation = rotation.Inverse();
            PerformAction(player);
            rotation = tempRot;
        }
    }

    public override void CheckIfActionIsPossible(PlayerController player, bool reverse = false) {
        canBePerformed = true;
    }

    public override void CheckIfBackwardActionIsPossible(PlayerController player) {
        canBePerformed = true;
    }
}