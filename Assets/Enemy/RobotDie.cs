using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotDie : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Renderer rend = animator.transform.Find("robot").GetComponent<Renderer>();
        rend.materials[1].SetColor("_Color", Color.black);
        Light light = animator.transform.Find("scanner").GetComponent<Light>();
        light.intensity = 0;

        var agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 0;
        agent.isStopped = true;
    }
}
