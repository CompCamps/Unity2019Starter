using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{

    private Animator anim;
    private InputMaster inputs;

    // bow - 0
    // one handed - 1
    // two handed - 2
    // stick - 3
    private int equiped = 0;

    void Awake()
    {
        inputs = new InputMaster();
        inputs.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        inputs.Player.Equip.performed += Equip;
        inputs.Player.Attack.performed += Attack;
    }

    private void Equip(InputAction.CallbackContext ctx)
    {
        if (equiped == 0)
        {
            equiped = 1;
        }
        else
        {
            equiped = 0;
        }
    }

    private void Attack(InputAction.CallbackContext ctx)
    {
        if (equiped != 0 && !anim.GetBool("is_sprinting"))
        {
            anim.SetTrigger("attack");
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("weapon", equiped);
    }

    void ShowOneHanded()
    {
        transform.Find("RPG/One-handed Sword").GetComponent<SkinnedMeshRenderer>().enabled = true;
        var bone = transform.Find("RPG/Root/Pelvis/Spine_01/Spine_02/Spine_03/Clavicle_L/UpperArm_L/LowerArm_L/Hand_L/One_Handed_Weapon_L");
        bone.GetComponent<CapsuleCollider>().enabled = true;
    }

    void HideOneHanded()
    {
        transform.Find("RPG/One-handed Sword").GetComponent<SkinnedMeshRenderer>().enabled = false;
        var bone = transform.Find("RPG/Root/Pelvis/Spine_01/Spine_02/Spine_03/Clavicle_L/UpperArm_L/LowerArm_L/Hand_L/One_Handed_Weapon_L");
        bone.GetComponent<CapsuleCollider>().enabled = false;
    }
}
