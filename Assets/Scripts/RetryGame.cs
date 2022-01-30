using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public bool gameOver = false;
    public bool gameWon = false;
    public Arduino_test arduino_Data = null;

    private void Update()
    {
        if (gameWon && (Input.GetKeyDown(KeyCode.Return) || arduino_Data.data == "1" || Input.GetKeyDown(KeyCode.Alpha1)))
        {
            SceneManager.LoadScene(0);
        }
        if (gameOver && (arduino_Data.data == "1" || Input.GetKeyDown(KeyCode.Alpha1)) && !gameWon)
        {
            SceneManager.LoadScene(1);
        }
        
    }

}
