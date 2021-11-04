using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConditionalAction", menuName = "ScriptableObjects/Actions/ConditionalAction", order = 1)]
public class ConditionalAction : Action_Voleur {
    public string tagCondition;
    public Tools.Directions direction;
    public bool addPlayerDirection = false;

    override public void PerformAction(PlayerController player, bool reverse = false) {
        if (reverse) {
            PerformActionBackward(player);
            return;
        }

        Tools.Directions tempDir = direction;
        if (addPlayerDirection) {
            tempDir = direction.Add(player.orientation);
        }
        if (player.actualSpot.TagInDirection(tempDir, tagCondition)) {
            actionDone = true;
        }
    }

    override public void PerformActionBackward(PlayerController player) {
        if (addPlayerDirection) {
            PerformAction(player);
        } else {
            Tools.Directions tempDir = direction;
            direction = direction.Inverse();
            PerformAction(player);
            direction = tempDir;
        }
    }

    override public void CheckIfActionIsPossible(PlayerController player, bool reverse = false) {
        if (reverse) {
            CheckIfBackwardActionIsPossible(player);
            return;
        }

        if (player.actualSpot != null) { canBePerformed = true; }
        else { canBePerformed = false; }
    }

    override public void CheckIfBackwardActionIsPossible(PlayerController player) {
        CheckIfActionIsPossible(player);
    }
}