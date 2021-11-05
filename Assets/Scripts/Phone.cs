using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public bool isDringDring = false;
    public string PhoneNumberString = "";
    [SerializeField] private float _duration = 10f;
    private float _timer = 0f;
    private bool _startBlaBla = false;

    public AudioClip[] soundPickUpPhone;
    public AudioClip[] soundHangUpPhone;
    private AudioSource _audioSource = null;
    private int randomSoundPickUp;
    private int randomSoundHangUp;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource)
        {
            Debug.LogError("You need an AudioSource for sounds");
        }
        randomSoundPickUp = Random.Range(0, soundPickUpPhone.Length);
        randomSoundHangUp = Random.Range(0, soundHangUpPhone.Length);
    }

    private void Update()
    {
        if (isDringDring)
        {
            _timer += Time.deltaTime;
            transform.rotation = Quaternion.Euler(-10f, 0f, 0f);
            StartCoroutine(Rotator());
            //Play SFX
            //Play Phone Anim

            if (_timer >= _duration && !_startBlaBla)
                StopDringDring();

            if (Input.GetKeyDown(KeyCode.E))
            {
                OnAllo();
                _startBlaBla = true;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                StopBlaBla();
                _startBlaBla = false;
            }
        }
    }

    public void OnAllo()
    {
        StopDringDring();
        if (_audioSource)
        {
            _audioSource.Stop();
            _audioSource.clip = null;
            _audioSource.clip = soundPickUpPhone[randomSoundPickUp];
            _audioSource.Play();
            int rand = randomSoundPickUp;
            while (rand == randomSoundPickUp)
            {
                rand = Random.Range(0, soundPickUpPhone.Length);
            }
            randomSoundPickUp = rand;
        }
    }

    public void StopBlaBla()
    {
        if (_audioSource)
        {
            _audioSource.Stop();
            _audioSource.clip = null;
            _audioSource.clip = soundHangUpPhone[randomSoundHangUp];
            _audioSource.Play();
            int rand = randomSoundHangUp;
            while (rand == randomSoundHangUp)
            {
                rand = Random.Range(0, soundHangUpPhone.Length);
            }
            randomSoundHangUp = rand;
        }
    }

    public void StopDringDring()
    {
        PhoneManager.instance.onePhoneDringDring = false;
        isDringDring = false;
        _timer = 0f;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    IEnumerator Rotator()
    {
        transform.rotation = Quaternion.Euler( new Vector3(Mathf.PingPong(Time.time * 150f, 10f), 0f, 0f) );
        yield return new WaitForSeconds(1f);
    }
}
