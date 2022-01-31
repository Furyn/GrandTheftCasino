using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLaunching : MonoBehaviour
{
    public Arduino_test arduino = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Space) || arduino.data == "1")
        {
            SceneManager.LoadScene("Baptiste LD");
        }
    }
}
