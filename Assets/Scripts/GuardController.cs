using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    public Transform[] passPoint;
    public NavMeshAgent agent;
    public float distance_trigger_phone = 5;
    public float wait_on_phone = 2f;

    private bool onPhone = false;
    private float timer_on_phone = 0f;
    private int index = 0;
    private PhoneManager phoneManager = null;

    public AudioClip[] soundHangUpPhone;
    private AudioSource _audioSource = null;
    private int randomSound;

    [SerializeField] private Animator animatorFront;
    [SerializeField] private Animator animatorBack;

    void Start()
    {
        phoneManager = FindObjectOfType<PhoneManager>();
        if (phoneManager == null)
        {
            Debug.LogError("No phone manager found");
        }

        if (passPoint.Length > index)
        {
            agent.SetDestination(passPoint[index].position);
        }

        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource)
        {
            Debug.LogError("You need an AudioSource for sounds");
        }
        randomSound = Random.Range(0, soundHangUpPhone.Length);
    }

    void Update()
    {
        if (onPhone)
        {
            timer_on_phone -= Time.deltaTime;
            if (timer_on_phone <= 0f)
            {
                onPhone = false;
                timer_on_phone = 0f;
                agent.SetDestination(passPoint[index].position);
            }
        }
        else
        {
            foreach (Phone phone in phoneManager.spawnPhoneInfo)
            {
                float distance = Vector3.Distance(phone.transform.position, agent.gameObject.transform.position);
                if (distance_trigger_phone > distance && phone.isDringDring)
                {
                    agent.SetDestination(phone.transform.position);
                    animatorFront.SetTrigger("isWalking");
                    animatorBack.SetTrigger("isWalking");
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            animatorFront.SetTrigger("isWalking");
                            animatorBack.SetTrigger("isWalking");
                            timer_on_phone = wait_on_phone;
                            onPhone = true;
                            StartCoroutine(PhoneSystem(phone));
                            if (_audioSource)
                            {
                                _audioSource.Stop();
                                _audioSource.clip = null;
                                _audioSource.clip = soundHangUpPhone[randomSound];
                                _audioSource.Play();
                                int rand = randomSound;
                                while (rand == randomSound)
                                {
                                    rand = Random.Range(0, soundHangUpPhone.Length);
                                }
                                randomSound = rand;
                            }
                        }
                    }
                }
            }
        }

        
    }

    IEnumerator PhoneSystem(Phone phone)
    {
        phone.OnAllo();
        yield return new WaitForSeconds(10f);
        phone.StopBlaBla();
        yield return new WaitForSeconds(2f);
    }
}
