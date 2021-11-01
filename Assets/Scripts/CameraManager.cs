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
    }
}
