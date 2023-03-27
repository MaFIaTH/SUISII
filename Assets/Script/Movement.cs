using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigid;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float maxMoveVelocity = 7f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private KeyCode jumpKey = KeyCode.W;
    [SerializeField] private string axisName = "Horizontal P1";

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float raycastDistance = 0.1f;
    private BoxCollider2D _boxCollider2d;


    private void Awake()
    {
        _boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis(axisName) * moveSpeed * Time.deltaTime, 0);
        playerRigid.velocity = new Vector2(Mathf.Clamp(playerRigid.velocity.x, -maxMoveVelocity, maxMoveVelocity),
            playerRigid.velocity.y);
        Debug.Log(playerRigid.velocity);
        playerRigid.AddForce(movement);
    }

    private bool IsGrounded()
    {
        var bounds = _boxCollider2d.bounds;
        RaycastHit2D[] raycastHit = Physics2D.BoxCastAll(bounds.center, bounds.size, 0f,
            Vector2.down, raycastDistance, platformLayerMask);
        bool hit = raycastHit.Any(x => x.collider != _boxCollider2d);
        Color rayColor = hit ? Color.green : Color.red;
        Debug.DrawRay(_boxCollider2d.bounds.center + new Vector3(bounds.extents.x, 0),
            Vector2.down * (bounds.extents.y + raycastDistance), rayColor);
        Debug.DrawRay(_boxCollider2d.bounds.center - new Vector3(bounds.extents.x, 0),
            Vector2.down * (bounds.extents.y + raycastDistance), rayColor);
        Debug.DrawRay(
            _boxCollider2d.bounds.center - new Vector3(bounds.extents.x, bounds.extents.y),
            Vector2.right * bounds.extents.x, rayColor);
        return hit;
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(jumpKey))
        {
            Vector2 jump = new Vector2(0, jumpForce);
            playerRigid.AddForce(jump, ForceMode2D.Impulse);
        }
    }
    

    private void Update()
    {
        Move();
        Jump();
    }
}
