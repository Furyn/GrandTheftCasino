using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public string gt;

    [SerializeField] public Phone[] spawnPhoneInfo;

    void Update()
    {
         if (gt.Length == 3) // enter/return
        {
            print("le numero appelé est " + gt);
            CallPhoneNumber(gt);
            gt = ("");
        }

        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (gt.Length != 0)
                {
                    gt = gt.Substring(0, gt.Length - 1);
                }
            }
            else
            {
                gt += c;
                Debug.Log(c);
            }
        }
    }
    public void CallPhoneNumber(string Number)
    {
        if (spawnPhoneInfo.Length != 0)
        {
            foreach (Phone phoneCheck in spawnPhoneInfo)
            {
                if(phoneCheck.PhoneNumberString == Number)
                {
                    phoneCheck.isDringDring = true;
                    Debug.Log("tu as appelé le phone : " + phoneCheck.gameObject.name);
                }
            }

        }
        Debug.Log("j'ai call le numero" + Number);
    }


}
