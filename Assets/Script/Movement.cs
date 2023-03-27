using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigid;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private KeyCode jumpKey = KeyCode.W;
    [SerializeField] private string axisName = "Horizontal P1";

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] float extraHeightText = 0.1f;
    private bool _canJump = true;
    private BoxCollider2D boxCollider2d;


    private void Awake()
    {
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis(axisName) * moveSpeed * Time.deltaTime, 0);
        playerRigid.AddForce(movement);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d .bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x,0), Vector2.down*(boxCollider2d.bounds.extents.y + extraHeightText),rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x,0), Vector2.down*(boxCollider2d.bounds.extents.y + extraHeightText),rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y), Vector2.right*(boxCollider2d.bounds.extents.x),rayColor);
        
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(jumpKey))
        {
            Vector2 jump = new Vector2(0, jumpForce);
            playerRigid.AddForce(jump, ForceMode2D.Impulse);
        }
        
        /*if (Input.GetKeyDown(jumpKey) && _canJump)
        {
            Vector2 jump = new Vector2(0, jumpForce);
            playerRigid.AddForce(jump, ForceMode2D.Impulse);
            _canJump = false;
        }*/
    }
    

    private void Update()
    {
        Move();
        Jump();
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            _canJump = true;
        }
    }
    */
}
