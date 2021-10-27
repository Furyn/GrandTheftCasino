using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField]
    private GameObject visionZone;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, other.transform.position, out hit))
            {

                if (!hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("C4EST UN MUR");
                }
            }
            else
            {
                // Game over
                Debug.Log("CEST LE JOEUUR");
            }
        }
    }
}
