using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotChase : StateMachineBehaviour
{

    NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Renderer rend = animator.transform.Find("robot").GetComponent<Renderer>();
        rend.materials[1].SetColor("_Color", Color.red);
        Light light = animator.transform.Find("scanner").GetComponent<Light>();
        light.color = Color.red;

        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 6.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var brain = animator.GetComponent<RobotBrain>();

        agent.SetDestination(brain.last_known);
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
