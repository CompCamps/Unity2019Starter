using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{

    private Animator anim;
    private InputMaster inputs;

    public GameObject oneHandedWeapon;

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

    private void Equip(InputAction.CallbackContext obj)
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
        if (equiped != 0)
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
        oneHandedWeapon.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

    void HideOneHanded()
    {
        oneHandedWeapon.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }
}
