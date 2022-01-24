using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCheck : MonoBehaviour
{

    public Image WinImage;
    [SerializeField] private RetryGame retry = null;
    private void OnTriggerEnter(Collider other)
    {     
        if (other.CompareTag("Player") == true)
        {
            if (WinImage)
            {
                WinImage.gameObject.SetActive(true);
                StartCoroutine(FadeImage(WinImage));
            }
        }          
    }

    IEnumerator FadeImage(Image img)
    {
        if (img)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }

            retry.gameWon = true;

        }
    }
}
