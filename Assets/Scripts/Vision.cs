using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vision : MonoBehaviour
{

    public Image gameOverImage;
    public AudioClip soundSeeRobber;
    public Transform raycastPos;
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
        if (other.CompareTag("Player") == true)
        {
            RaycastHit hit;
            if (Physics.Linecast(raycastPos.position, other.transform.position, out hit))
            {
                if (!hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
                else
                {
                    if (_audioSource)
                    {
                        _audioSource.Stop();
                        _audioSource.clip = soundSeeRobber;
                        _audioSource.Play();

                        // Game over
                        if (gameOverImage)
                        {
                            gameOverImage.gameObject.SetActive(true);
                            StartCoroutine(FadeImage(gameOverImage));
                        }
                    }
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
