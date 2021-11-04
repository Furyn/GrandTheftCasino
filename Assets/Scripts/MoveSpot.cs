using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpot : MonoBehaviour
{
    public float distanceDetection = 1.0f;
    public List<GameObject> spotInfos;
    public List<MoveSpot> spotDirections;

    void Start()
    {
        UpdateInfos();
    }


    // Récupère les informations des différents obstacles autour du point 
    void UpdateInfos() {
        RaycastHit[][] directionsObject = new RaycastHit[4][];
        directionsObject[0] = Physics.RaycastAll(transform.position, new Vector3(0, 0, 1), distanceDetection);
        //Debug.Log(directionsObject[0]);
        directionsObject[1] = Physics.RaycastAll(transform.position, new Vector3(1, 0, 0), distanceDetection);
        directionsObject[2] = Physics.RaycastAll(transform.position, new Vector3(0, 0, -1), distanceDetection);
        directionsObject[3] = Physics.RaycastAll(transform.position, new Vector3(-1, 0, 0), distanceDetection);

        for (int i = 0; i < directionsObject.Length; i++) {
            if (directionsObject[i].Length == 0 || directionsObject[i][0].collider) {
                spotInfos.Add(null);
            } else {
                spotInfos.Add(directionsObject[i][0].collider.gameObject);
            }
        }
    }


    // Vérifie pour chaque direction s'il est encore possible de s'y déplacer
    public Dictionary<GameObject, bool> GetInfos()
    {
        Dictionary<GameObject, bool> infos = new Dictionary<GameObject, bool>();
        for (int i = 0; i < spotInfos.Count; i++)
        {
            if (Vector3.Distance(spotInfos[i].transform.position, transform.position) <= 1.0f) infos.Add(spotInfos[i], false);
            else infos.Add(spotInfos[i], true);
        }

        return infos;
    }

    public bool TagInDirection(Tools.Directions direction, string tag) {
        GameObject go = spotInfos[(int)direction];
        if (go != null && go.CompareTag(tag)) {
            return true;
        }
        return false;
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        float size = GetComponent<SphereCollider>().radius;
        Gizmos.DrawWireSphere(transform.position, size);
    }

    public void OnDrawGizmosSelected() {
        Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow };
        for (int i = 0; i < spotDirections.Count; i++) {
            if (spotDirections[i] == null) { continue; }
            Gizmos.color = colors[i];
            Gizmos.DrawLine(transform.position, spotDirections[i].transform.position);
        }
    }
}
