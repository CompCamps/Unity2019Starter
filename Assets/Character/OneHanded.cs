using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHanded : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var anim = player.GetComponent<Animator>();
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack1(WeaponOneHand)"))
            {
                other.GetComponent<RobotBrain>().health -= 5;
            } else
            {
                Debug.Log(other);
                other.GetComponent<RobotBrain>().health -= 0.5f;
            }
        }
    }
}
