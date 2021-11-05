using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class Arduino_test : MonoBehaviour
{
    public string data = "";
    public string input_telephone = "";
    public bool OnCall = false;
    public bool upCam = false;
    public bool downCam = false;

    void Start()
    {
        /*UduinoManager.Instance.pinMode(12, PinMode.Output);
        UduinoManager.Instance.pinMode(13, PinMode.Output);
        UduinoManager.Instance.pinMode(11, PinMode.Output);
        UduinoManager.Instance.pinMode(10, PinMode.Output);
        UduinoManager.Instance.pinMode(9, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(8, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(6, PinMode.Input_pullup);*/
    }

    // Update is called once per frame
    void Update()
    {
        /*UduinoManager.Instance.digitalWrite(13, State.HIGH);
        UduinoManager.Instance.digitalWrite(12, State.HIGH);
        UduinoManager.Instance.digitalWrite(11, State.HIGH);
        UduinoManager.Instance.digitalWrite(10, State.HIGH);*/
        
        //if (UduinoManager.Instance.uduinoDevices["uduinoBoard"] != null)
        if (UduinoManager.Instance.uduinoDevices.ContainsKey("uduinoBoard"))
        {
            UduinoManager.Instance.sendCommand("pr");
            data = UduinoManager.Instance.uduinoDevices["uduinoBoard"].ReadFromArduino();

            if (data == "STOP")
            {
                OnCall = false;
            }
            else if (data == "ONCALL")
            {
                OnCall = true;
            }
            else if (data == "NO+")
            {
                upCam = false;
            }
            else if (data == "+")
            {
                upCam = true;
            }
            else if (data == "NO-")
            {
                downCam = false;
            }
            else if (data == "-")
            {
                downCam = true;
            }
            else if (data != null && OnCall)
            {
                input_telephone += data;
                PhoneManager.instance.gt = input_telephone;
                if (input_telephone.Length >= 3) { input_telephone = ""; }
            }

            UduinoManager.Instance.sendCommand("ref");
        }

        /*UduinoManager.Instance.digitalWrite(13, State.LOW);
        if (UduinoManager.Instance.digitalRead(9) == 1) { input_telephone += "#"; return; }
        else if (UduinoManager.Instance.digitalRead(6) == 1) { input_telephone += "*"; return; }
        else if (UduinoManager.Instance.digitalRead(8) == 1) { input_telephone += "0"; return; }
        UduinoManager.Instance.digitalWrite(13, State.HIGH);

        UduinoManager.Instance.digitalWrite(12, State.LOW);
        if (UduinoManager.Instance.digitalRead(9) == 1) { input_telephone += "3"; return; }
        else if (UduinoManager.Instance.digitalRead(8) == 1) { input_telephone += "2"; return; }
        else if (UduinoManager.Instance.digitalRead(6) == 1) { input_telephone += "1"; return; }
        UduinoManager.Instance.digitalWrite(12, State.HIGH);

        UduinoManager.Instance.digitalWrite(11, State.LOW);
        if (UduinoManager.Instance.digitalRead(9) == 1) { input_telephone += "6"; return; }
        else if (UduinoManager.Instance.digitalRead(8) == 1) { input_telephone += "5"; return; }
        else if (UduinoManager.Instance.digitalRead(6) == 1) { input_telephone += "4"; return; }
        UduinoManager.Instance.digitalWrite(11, State.HIGH);

        UduinoManager.Instance.digitalWrite(10, State.LOW);
        if (UduinoManager.Instance.digitalRead(9) == 1) { input_telephone += "9"; return; }
        else if (UduinoManager.Instance.digitalRead(8) == 1) { input_telephone += "8"; return; }
        else if (UduinoManager.Instance.digitalRead(6) == 1) { input_telephone += "7"; return; }
        UduinoManager.Instance.digitalWrite(10, State.HIGH);*/
    }
}
