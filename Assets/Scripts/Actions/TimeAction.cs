using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TimedAction", menuName = "ScriptableObjects/Actions/TimedAction", order = 1)]
public class TimeAction : Action_Voleur {
    public float timeToWait;

    override public void PerformAction(PlayerController player, bool reverse = false) {
        actionDone = false;
        player.StartCoroutine(WaitCoroutine());
    }

    override public void PerformActionBackward(PlayerController player) {
        PerformAction(player);
    }

    IEnumerator WaitCoroutine() {
        yield return new WaitForSeconds(timeToWait);
        actionDone = true;
    }

    override public void CheckIfActionIsPossible(PlayerController player, bool reverse = false) {
        canBePerformed = true;
    }

    override public void CheckIfBackwardActionIsPossible(PlayerController player) {
        canBePerformed = true;
    }
}
