using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    
    MoveSpot spot;


    
    override public void PerformAction(GameObject player)
    {
        Vector3.MoveTowards(player.transform.position,new Vector3(spot.transform.position.x, player.transform.position.y, player.transform.position.z), 0.2f);
        if (Vector3.Distance(player.transform.position, spot.transform.position) <= 0.3f) actionDone = true;
    }

    override public void CheckIfActionIsPossible(GameObject player)
    {
        RaycastHit[] target = Physics.RaycastAll(player.transform.position, spot.transform.position - player.transform.position);
        if (target[0].collider.gameObject.tag == "Spot") canBePerformed = true;
        else canBePerformed = false;
    }
}
