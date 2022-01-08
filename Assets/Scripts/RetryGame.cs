using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public bool gameOver = false;
    public Arduino_test arduino_Data = null;

    private void Update()
    {
        if (gameOver && arduino_Data.data == "1" || Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(1);
        }
    }

}
