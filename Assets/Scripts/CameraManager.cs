using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera[] cameras;

    private int actualCamera = 0;

    [SerializeField]
    private GameObject cameraText;

    [SerializeField]
    private GameObject timeText;

    public System.TimeSpan timeSpan = new System.TimeSpan(0,0,0,0,0);

    [SerializeField]
    private float timeRate = 1;

    [SerializeField]
    private GameObject tvStatic;

    private void Update()
    {
        Debug.Log(Time.time);
        float milliseconds = Time.deltaTime * 1000 * timeRate;
        timeSpan += new System.TimeSpan(0, 0, 0, 0, (int)milliseconds);

        timeText.GetComponentInChildren<TextMeshProUGUI>().text = timeSpan.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
            TurnStaticOn();

        // Fait par DOV
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeCamera(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeCamera(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeCamera(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ChangeCamera(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ChangeCamera(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            ChangeCamera(6);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            ChangeCamera(7);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            ChangeCamera(8);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            ChangeCamera(9);
        }
        //if (Input.GetKeyDown(KeyCode.Keypad0))
        //{
          //  ChangeCamera(0);
        //}

        cameraText.GetComponentInChildren<TextMeshProUGUI>().text = "CAM. " + (actualCamera + 1);

    }

    private void ChangeCamera(int cameraToChange)
    {
        if((cameraToChange - 1) == -1 )
        {
            Debug.Log("ahah je vous ai bien niqué");
        }
        Debug.Log("C'est la camera " + cameraToChange);
        cameras[actualCamera].enabled = false;
        actualCamera = cameraToChange  - 1;
        cameras[actualCamera].enabled = true;


    }

    private void TurnStaticOn()
    {
        tvStatic.SetActive(true);
        Invoke("TurnStaticOff", .25f);
        Debug.Log("Fonction faite");
    }

    private void TurnStaticOff()
    {
        Debug.Log("Ca fait 5");
        tvStatic.SetActive(false);
    }


    /* OLD CODE
    void Update()
    {
        //Debug.Log(actualCamera);
        float milliseconds = Time.deltaTime * 1000 * timeRate;
        timeSpan += new System.TimeSpan(0, 0, 0, 0, (int)milliseconds);

        timeText.GetComponentInChildren<TextMeshProUGUI>().text = timeSpan.ToString();

        if (Input.GetKeyDown(KeyCode.UpArrow))
            SelectNextCamera();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            SelectPreviousCamera();

        cameraText.GetComponentInChildren<TextMeshProUGUI>().text = "CAM. " + (actualCamera + 1);
    }
    private void SelectNextCamera()
    {
        cameras[actualCamera].enabled = false;

        if (actualCamera == cameras.Length - 1)
        {
            actualCamera = 0;
            //Debug.Log(actualCamera);
        }
        else
        {
            actualCamera = actualCamera + 1;
            //Debug.Log(actualCamera);
        }

        cameras[actualCamera].enabled = true;
    }


    private void SelectPreviousCamera()
    {
        cameras[actualCamera].enabled = false;

        if (actualCamera <= 0)
        {
            actualCamera = cameras.Length - 1;
            //Debug.Log(actualCamera);
        }
        else
        {
            actualCamera = actualCamera - 1;
            //Debug.Log(actualCamera);
        }

        cameras[actualCamera].enabled = true;
    }*/
}
