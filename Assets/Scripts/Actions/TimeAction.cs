using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TimedAction", menuName = "ScriptableObjects/Actions/TimedAction", order = 1)]
public class TimeAction : Action_Voleur {
    public float timeToWait;

    override public void PerformAction(PlayerController player, bool reverse = false) {
        done = false;
        player.StartCoroutine(WaitCoroutine());
    }

    override public bool CheckIfActionIsPossible(PlayerController player, bool reverse = false) {
        return true;
    }

    IEnumerator WaitCoroutine() {
        yield return new WaitForSeconds(timeToWait);
        done = true;
    }
}
