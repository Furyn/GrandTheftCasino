using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public string gt;
    [SerializeField] public Phone[] spawnPhoneInfo;

    public bool onePhoneDringDring = false;

    public static PhoneManager instance = null;

    public AudioClip[] soundWrongNumber;
    public AudioClip soundRinging;
    private AudioSource _audioSource = null;
    private bool wrongNum;
    private int randomSound;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource)
        {
            Debug.LogError("You need an AudioSource for sounds");
        }
        randomSound = UnityEngine.Random.Range(0, soundWrongNumber.Length);
    }

    void Update()
    {
        if (!RecognitionVoice.instance.recording && gt.Length == 3) // enter/return
        {
            if (!onePhoneDringDring)
                CallPhoneNumber(gt);
            gt = ("");
        }

        if (_audioSource != null) {
            if (!onePhoneDringDring && _audioSource.isPlaying && !wrongNum) {
                if (_audioSource) {
                    _audioSource.Stop();
                    _audioSource.clip = null;
                }
            }
        }

        //foreach (char c in Input.inputString)
        //{
        //    if (c == '\b') // has backspace/delete been pressed?
        //    {
        //        if (gt.Length != 0)
        //            gt = gt.Substring(0, gt.Length - 1);
        //    }
        //    else
        //        gt += c;
        //}
    }
    public void CallPhoneNumber(string Number)
    {
        if (spawnPhoneInfo.Length != 0)
        {
            int check = 0;
            foreach (Phone phoneCheck in spawnPhoneInfo)
            {
                if (phoneCheck.PhoneNumberString == Number)
                {
                    phoneCheck.isDringDring = true;
                    onePhoneDringDring = true;
                    Debug.Log("tu as appelé le phone : " + phoneCheck.gameObject.name);
                }
                else
                {
                    check++;
                }
            }
            if (check == spawnPhoneInfo.Length)// Call wrong number
            {
                if (_audioSource)
                {
                    wrongNum = true;
                    _audioSource.Stop();
                    _audioSource.clip = null;
                    _audioSource.clip = soundWrongNumber[randomSound];
                    _audioSource.Play();
                    int rand = randomSound;
                    while (rand == randomSound)
                    {
                        rand = UnityEngine.Random.Range(0, soundWrongNumber.Length);
                    }
                    randomSound = rand;
                    Debug.Log(_audioSource.clip);
                }
            }
            else
            {
                if (_audioSource)
                {
                    wrongNum = false;
                    Debug.Log("fbejfbjkb");
                    _audioSource.Stop();
                    _audioSource.clip = null;
                    _audioSource.clip = soundRinging;
                    _audioSource.Play();
                }
            }

        }
        Debug.Log("Numero dials : " + Number);
    }


}
