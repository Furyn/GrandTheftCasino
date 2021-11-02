using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpot : MonoBehaviour
{

    private List<GameObject> spotInfos;
    void Start()
    {
        UpdateInfos();
    }


    // Récupère les informations des différents obstacles autour du point 
    void UpdateInfos()
    {
        RaycastHit[] upObject = Physics.RaycastAll(transform.position, new Vector3(0, 0, 1));
        RaycastHit[] rightObject = Physics.RaycastAll(transform.position, new Vector3(1, 0, 0));
        RaycastHit[] downObject = Physics.RaycastAll(transform.position, new Vector3(0, 0, -1));
        RaycastHit[] leftObject = Physics.RaycastAll(transform.position, new Vector3(-1, 0, 0));
        
        spotInfos.Add(upObject[0].collider.gameObject);
        spotInfos.Add(rightObject[0].collider.gameObject);
        spotInfos.Add(downObject[0].collider.gameObject);
        spotInfos.Add(leftObject[0].collider.gameObject);

    }


    // Vérifie pour chaque direction s'il est encore possible de s'y déplacer
    Dictionary<GameObject, bool> GetInfos()
    {
        Dictionary<GameObject, bool> infos = new Dictionary<GameObject, bool>();
        for (int i = 0; i < spotInfos.Count; i++)
        {
            if (Vector3.Distance(spotInfos[i].transform.position, transform.position) <= 1.0f) infos.Add(spotInfos[i], false);
            else infos.Add(spotInfos[i], true);
        }

        return infos;
    }


}
