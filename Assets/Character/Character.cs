using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public float speed = 1;
    public float rotationSpeed = 80;

    float rotation = 0;
    float gravity = 0.6f;
    float vertical = 0f;
    bool jumped = false;

    private float moveSpeed = 0;
    Vector3 moveVec = Vector3.zero;

    CharacterController controller;
    Animator anim;

    private InputAction movement;
    private InputAction sprint;

    private InputMaster inputs;
    void Awake()
    {
        inputs = new InputMaster();
        inputs.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        //controls.Player.Movement.performed += ctx => Move();
        //controls.Player.Equip.performed += _ => Equip();

        movement = inputs.Player.Movement;
        movement.performed += Move;
        movement.canceled += StopMove;

        sprint = inputs.Player.Sprinting;
        sprint.performed += StartSprinting;
        sprint.canceled += StopSprinting;

        inputs.Player.Jump.performed += Jump;

        inputs.Player.Camera.performed += RotateCamera;

    }

    void Move(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        moveSpeed = value;
        anim.speed = value * speed;
        anim.SetBool("is_walking", true);
    }

    void StopMove(InputAction.CallbackContext ctx)
    {
        moveSpeed = 0;
        anim.SetBool("is_walking", false);
        anim.speed = 1 * speed;
    }

    void StartSprinting(InputAction.CallbackContext ctx)
    {
        anim.SetBool("is_sprinting", true);
    }

    void StopSprinting(InputAction.CallbackContext ctx)
    {
        anim.SetBool("is_sprinting", false);
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        if (controller.isGrounded)
        {
            anim.SetTrigger("jump");
            vertical = 6f;
            jumped = true;
        }
    }

    void RotateCamera(InputAction.CallbackContext ctx)
    {
        rotation += ctx.ReadValue<float>() * rotationSpeed * Time.deltaTime * -1;
        transform.eulerAngles = new Vector3(0, rotation, 0);
    }

    bool isFalling()
    {
        return !Physics.Raycast(transform.position, -Vector3.up, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        // transform.eulerAngles = new Vector3(0, rotation, 0);

        if (controller.isGrounded)
        {
            var moveDir = new Vector3(0, 0, 1) * moveSpeed * 2;
            anim.SetBool("is_falling", false);
            if (anim.GetBool("is_sprinting"))
            {
                moveDir *= 5f;
            }

            moveVec = transform.TransformDirection(moveDir);
        }
        else if (isFalling() && !anim.GetBool("is_falling"))
        {
            anim.SetBool("is_falling", true);
        }

        if (vertical > 0)
        {
            vertical -= gravity;
        } else
        {
            vertical = 0f;
        }

        moveVec.y += vertical - gravity;
        controller.Move(moveVec * Time.deltaTime);
    }
}
