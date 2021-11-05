using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public AudioClip soundSeeRobber;
    private AudioSource _audioSource = null;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource)
        {
            Debug.LogError("You need an AudioSource for sounds");
        }
        else
        {
            _audioSource.clip = soundSeeRobber;
        }
    }
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
                if (_audioSource)
                {
                    _audioSource.Play();
                }
                Debug.Log("CEST LE JOEUUR");
            }
        }
    }
}
