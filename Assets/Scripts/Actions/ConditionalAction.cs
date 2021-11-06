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
            done = true;
        }
    }

    private void PerformActionBackward(PlayerController player) {
        if (addPlayerDirection) {
            PerformAction(player);
        } else {
            Tools.Directions tempDir = direction;
            direction = direction.Inverse();
            PerformAction(player);
            direction = tempDir;
        }
    }

    override public bool CheckIfActionIsPossible(PlayerController player, bool reverse = false) {
        if (player.actualSpot != null) { return true; }
        return false;
    }
}