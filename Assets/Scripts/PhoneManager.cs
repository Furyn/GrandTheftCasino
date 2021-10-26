using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{

    public string gt;

    [Serializable]
    public class PhoneInfo
    {
        public GameObject PhoneObject;
      //  public int PhoneNumber;
        public string PhoneNumberString;
    }

    [SerializeField] public PhoneInfo[] spawnPhoneInfo;

    

    void Start()
    {
       // gt = GetComponent<string>();
    }

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
            foreach (PhoneInfo PhoneCheck in spawnPhoneInfo)
            {
                if(PhoneCheck.PhoneNumberString == Number)
                {
                    Debug.Log("tu as appelé le phone : " + PhoneCheck.PhoneObject);
                    PhoneCheck.PhoneObject.transform.localScale = new Vector3(2, 2, 2);
                }
                //Debug.Log(repl.PhoneNumberString + " " + repl.PhoneObject);
            }

        }
            //Debug.Log("la taille de spawn info est "+spawnPhoneInfo.); 


        Debug.Log("j'ai call le numero" + Number);
    }


}
