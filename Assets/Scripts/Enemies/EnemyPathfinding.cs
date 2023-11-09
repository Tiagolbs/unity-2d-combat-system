using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    
    private Rigidbody2D myRigidbody;
    private Vector2 moveDirection;
    private Knockback knockBack;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<Knockback>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (knockBack.GettingKnockedBack)
        {
            return;
        }
        myRigidbody.MovePosition(myRigidbody.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 roamingPosition)
    {
        moveDirection = roamingPosition;
    }
}
