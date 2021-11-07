using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vision : MonoBehaviour
{

    public Image gameOverImage;
    public AudioClip soundSeeRobber;
    private AudioSource _audioSource = null;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource)
        {
            Debug.LogError("You need an AudioSource for sounds");
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
                else
                {
                    if (_audioSource)
                    {
                        _audioSource.Stop();
                        _audioSource.clip = soundSeeRobber;
                        _audioSource.Play();
                    }
                }
            }
            else
            {
                // Game over

                if (gameOverImage)
                {
                    gameOverImage.gameObject.SetActive(true);
                    StartCoroutine(FadeImage(gameOverImage));
                }

            }
        }
    }

    IEnumerator FadeImage(Image img)
    {
        if(img)
        {
            for(float i = 0; i <= 1; i+= Time.deltaTime)
            {
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
            
        }
    }
}
