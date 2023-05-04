using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NPCMove : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Transform moveTo;
    [SerializeField] float speed = 3f;

    Animator animator;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        moveTo = GameManeger.instance.player.transform;
    }

    private void FixedUpdate()
    {
        if(moveTo == null) { return; }
        if(Vector3.Distance(transform.position, moveTo.position) < 0.8f)
        {
            StopMoving();
            return;
        }
        Vector3 direction = (moveTo.position - transform.position).normalized;
        animator.SetFloat("horizontal", direction.x);
        animator.SetFloat("vertical", direction.y);
        direction *= speed;
        rb2d.velocity = direction;
    }

    private void StopMoving()
    {
        moveTo = null;
        rb2d.velocity = Vector3.zero;
    }
}
