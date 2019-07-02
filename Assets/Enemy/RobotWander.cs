using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotWander : StateMachineBehaviour
{

    public float wanderRadius;
    public float wanderTimer;

    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = 2;
        timer = wanderTimer;

        Renderer rend = animator.transform.Find("robot").GetComponent<Renderer>();
        rend.materials[1].SetColor("_Color", Color.blue);
        Light light = animator.transform.Find("scanner").GetComponent<Light>();
        light.color = Color.blue;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(animator.transform.position, wanderRadius, -1);
            agent.isStopped = true;
            agent.SetDestination(newPos);
            agent.isStopped = false;
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;

        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

}
