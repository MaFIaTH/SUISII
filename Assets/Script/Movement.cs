using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D PRb;

    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private KeyCode JumpKey = KeyCode.W;
    [SerializeField] private string axisName = "Horizontal P1";
    private bool canJump = true;

    private void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis(axisName) * moveSpeed * Time.deltaTime, 0);
        PRb.AddForce(movement);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(JumpKey) && canJump)
        {
            Vector2 jump = new Vector2(0, jumpForce);
            PRb.AddForce(jump, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            canJump = true;
        }
    }
    
}
