using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private bool facingLeft = false;

    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    
    public bool FacingLeft
    {
        get => facingLeft;
        set => facingLeft = value;
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        
        myAnimator.SetFloat(MoveX, movement.x);
        myAnimator.SetFloat(MoveY, movement.y);

    }

    private void Move()
    {
        myRigidbody.MovePosition(myRigidbody.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector2 mousePosition = playerControls.Movement.MousePosition.ReadValue<Vector2>();
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePosition.x < playerScreenPoint.x)
        {
            FacingLeft = true;
            mySpriteRenderer.flipX = true;
        }
        else
        {
            FacingLeft = false;
            mySpriteRenderer.flipX = false;
        }
    }
}
