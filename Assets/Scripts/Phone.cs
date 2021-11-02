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
        //Active Reco Vocal
        //stopMouvement
    }

    public void StopBlaBla()
    {
        //Desactive Reco Vocal
        //reactive mouvement
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
