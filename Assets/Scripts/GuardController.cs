using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    public Transform[] passPoint;
    public NavMeshAgent agent;
    private int index = 0;

    void Start()
    {
        if (passPoint.Length > index)
        {
            agent.SetDestination(passPoint[index].position);
        }
        
    }

    void Update()
    {

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    index++;
                    Debug.Log("HERE");
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

            
            /*
            */

            
        
    }
}
