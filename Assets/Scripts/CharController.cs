using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3;
    public float backwardSpeed = 1.5f; 
    public float rotationSpeed = 90;
    public float gravity = -20f;
    public float jumpSpeed = 1.5f;
    public float sprintMultiplier = 2; 

    [Header("Animation Settings")]
    public Animator animator;

    CharacterController characterController;
    Vector3 moveVelocity;
    Vector3 turnVelocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (characterController.isGrounded)
        {
            float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
            moveVelocity = transform.forward * currentSpeed * vInput;
            turnVelocity = transform.up * rotationSpeed * hInput;
            if (Input.GetButtonDown("Jump"))
            {
                moveVelocity.y = jumpSpeed;
                animator.SetTrigger("jump");
            }
            else if (vInput > 0)
            {
                animator.SetBool("walk", true);
                animator.SetBool("walkback", false);
                animator.SetBool("run", isSprinting);
                animator.SetBool("idle", false);
            }
            else if (vInput < 0)
            {
                animator.SetBool("walk", false);
                animator.SetBool("walkback", true);
                animator.SetBool("run", false);
                animator.SetBool("idle", false);
            }
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("walkback", false);
                animator.SetBool("run", false);
                animator.SetBool("idle", true);
            }
        }
        
        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }
}