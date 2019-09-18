using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input.Plugins.PlayerInput;

public class PlayerControl : MonoBehaviour
{
    private static readonly int Run = Animator.StringToHash("Run");

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform carryPosition;
    [SerializeField] private GameObject idleHands;
    [SerializeField] private GameObject grabHands;
    
    private CharacterController ctrl;
    private Animator animator;

    private GameObject detectedInteractable;
    private Vector2 move;
    private bool canMove = true; // Whether the character is allowed to move
    
    public Shape _carriedShape;

    public Transform CarryPosition => carryPosition;

    public Shape CarriedShape
    {
        get => _carriedShape;
        set
        {
            _carriedShape = value;
            idleHands.SetActive(_carriedShape == null);
            grabHands.SetActive(_carriedShape != null);
        }
    }

    private void Start()
    {
        ctrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Move the character
        var movement = new Vector3(move.x, 0, move.y);
        movement *= moveSpeed;
        movement += Vector3.down * 9.8f;
        ctrl.Move(movement);
        
        animator.SetBool(Run, move.magnitude > 0.1f);
        
        // Face the character towards the move direction
        if (move.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.y));
        }
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void OnInteract()
    {
        if (detectedInteractable != null)
        {
            if (detectedInteractable.GetComponent<IInteractable>().Interact(this))
            {
                detectedInteractable = null;
                return;
            }
        }
        // If carrying something and not in range of a machine, we interact with it instead
        if (CarriedShape != null)
        {
            CarriedShape.Interact(this);
        }
    }

    public void OnDeviceLost()
    {
        canMove = false;
    }

    public void OnDeviceRegained()
    {
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            detectedInteractable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == detectedInteractable)
        {
            detectedInteractable = null;
        }
    }
}
