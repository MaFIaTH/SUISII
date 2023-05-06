using System.Linq;
using UnityEngine;

namespace Script
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRigid;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float maxMoveVelocity = 7f;
        [SerializeField] private float decelerationRate = 0.5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private KeyCode jumpKey = KeyCode.W;
        [SerializeField] private string axisName = "Horizontal P1";
        [SerializeField] private LayerMask jumpableLayer;
        [SerializeField] private float raycastDistance = 0.1f;
        [SerializeField] private GameObject playerSprite;
        [SerializeField] private AudioClip jumpSound;
        private Animator _animator;
        private AudioSource _audioSource;
        private BoxCollider2D _boxCollider2d;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");


        private void Awake()
        {
            _boxCollider2d = GetComponent<BoxCollider2D>();
            _audioSource = GetComponent<AudioSource>();
            _animator = playerSprite.GetComponent<Animator>();
        }

        private void Update()
        {
            Move();
            FlipSprite();
            Jump();
        }

        /// <summary>
        /// Move the player and play the animation.
        /// </summary>
        private void Move()
        {
            Vector2 movement = new Vector2(Input.GetAxis(axisName) * moveSpeed * Time.deltaTime, 0);
            _animator.SetBool(IsWalking, Input.GetAxis(axisName) != 0);
            playerRigid.AddForce(movement, ForceMode2D.Force);
            playerRigid.velocity = new Vector2(Mathf.Clamp(playerRigid.velocity.x, -maxMoveVelocity, maxMoveVelocity),
                playerRigid.velocity.y);
            if (Input.GetAxis(axisName) != 0 || !(playerRigid.velocity.magnitude > 0)) return;
            Vector2 playerRigidVelocity = playerRigid.velocity;
            Vector2 decelerationVelocity = playerRigidVelocity;
            decelerationVelocity -= decelerationVelocity.normalized * decelerationRate;
            playerRigidVelocity = new Vector2(decelerationVelocity.x, playerRigidVelocity.y);
            playerRigid.velocity = playerRigidVelocity;
            if (playerRigid.velocity.magnitude < decelerationRate)
            {
                playerRigid.velocity = Vector2.zero;
            }
        }
        
        /// <summary>
        /// Flip the sprite according to the direction of the player.
        /// </summary>
        private void FlipSprite()
        {
            if (Input.GetAxis(axisName) > 0)
            {
                var localScale = playerSprite.transform.localScale;
                localScale = new Vector3(Mathf.Abs(localScale.x), localScale.y, localScale.z);
                playerSprite.transform.localScale = localScale;
            }
            else if (Input.GetAxis(axisName) < 0)
            {
                var localScale = playerSprite.transform.localScale;
                localScale = new Vector3(-Mathf.Abs(localScale.x), localScale.y, localScale.z);
                playerSprite.transform.localScale = localScale;
            }
        }

        /// <summary>
        /// Check if the player is on the ground
        /// </summary>
        /// <returns></returns>
        private bool IsGrounded()
        {
            var bounds = _boxCollider2d.bounds;
            RaycastHit2D[] raycastHit = Physics2D.BoxCastAll(bounds.center, bounds.size, 0f,
                Vector2.down, raycastDistance, jumpableLayer);
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

        /// <summary>
        ///  Make the player jump if the player is on the ground and the jump key is pressed.
        /// </summary>
        private void Jump()
        {
            if (!IsGrounded() || !Input.GetKeyDown(jumpKey)) return;
            Vector2 jump = new Vector2(0, jumpForce);
            playerRigid.AddForce(jump, ForceMode2D.Impulse);
            _animator.Play("Jump");
            float randomPitch = Random.Range(0.5f, 1.5f);
            _audioSource.pitch = randomPitch;
            _audioSource.PlayOneShot(jumpSound);
        }
    }
}
