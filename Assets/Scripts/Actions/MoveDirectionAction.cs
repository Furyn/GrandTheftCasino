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

    public void PerformActionBackward(PlayerController player) {
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
        if (reverse) {
            return CheckIfBackwardActionIsPossible(player);
        }

        Tools.Directions tempDir = direction;
        if (addPlayerDirection) {
            tempDir = direction.Add(player.orientation);
        }

        if (player.SpotInDirection(tempDir)) {
            return true;
        }
        return false;
    }

    public bool CheckIfBackwardActionIsPossible(PlayerController player) {
        Tools.Directions tempDir = direction;
        if (!addPlayerDirection) {
            tempDir = direction.Inverse();
        } else {
            tempDir = direction.Add(player.orientation);
        }

        if (player.SpotInDirection(tempDir) != null) {
            return true;
        }
        return false;
    }

    public void WorkDone() {
        done = true;
        Debug.LogWarning("WORK DONE !");
    }
}
