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
        
    }

    void Update()
    {
        if (onPhone)
        {
            timer_on_phone -= Time.deltaTime;
            if (timer_on_phone >= 0f)
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
                    timer_on_phone = wait_on_phone;
                    onPhone = true;
                }
            }
        }

        if (!agent.pathPending && !onPhone)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    index++;
                    if (passPoint.Length < index)
                    {
                        index = 0;
                    }
                    if (passPoint.Length > index)
                    {
                        agent.SetDestination(passPoint[index].position);
                    }
                }
            }
        }

    }
}
