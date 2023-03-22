using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigid;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private KeyCode jumpKey = KeyCode.W;
    [SerializeField] private string axisName = "Horizontal P1";
    private bool _canJump = true;

    private void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis(axisName) * moveSpeed * Time.deltaTime, 0);
        playerRigid.AddForce(movement);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(jumpKey) && _canJump)
        {
            Vector2 jump = new Vector2(0, jumpForce);
            playerRigid.AddForce(jump, ForceMode2D.Impulse);
            _canJump = false;
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
            _canJump = true;
        }
    }
    
}
