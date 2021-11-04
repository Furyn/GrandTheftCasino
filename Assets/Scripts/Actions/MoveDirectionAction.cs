using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConditionalAction", menuName = "ScriptableObjects/Actions/MoveDirectionAction", order = 1)]
public class MoveDirectionAction : Action_Voleur {
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
        
        player.StartCoroutine(Tools.Wait(player.MoveToSpotDirection(tempDir), WorkDone));
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

        Tools.Directions tempDir = direction;
        if (addPlayerDirection) {
            tempDir = direction.Add(player.orientation);
        }

        if (player.SpotInDirection(tempDir)) {
            canBePerformed = true;
            return;
        }
        canBePerformed = false;
    }

    override public void CheckIfBackwardActionIsPossible(PlayerController player) {
        Tools.Directions tempDir = direction;
        if (!addPlayerDirection) {
            tempDir = direction.Inverse();
        } else {
            tempDir = direction.Add(player.orientation);
        }

        if (player.SpotInDirection(tempDir) != null) {
            canBePerformed = true;
            return;
        }
        canBePerformed = false;
    }

    public void WorkDone() {
        actionDone = true;
    }
}
