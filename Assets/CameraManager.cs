using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private List<Camera> cameras = new List<Camera>();

    private int actualCamera = 0;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            actualCamera = actualCamera + 1;
            Debug.Log(actualCamera);
            if(actualCamera > cameras.Count)
            {

            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            actualCamera = actualCamera - 1;
            //if()
        }
    }
}
