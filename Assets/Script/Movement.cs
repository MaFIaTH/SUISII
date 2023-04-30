using System.Linq;
using UnityEngine;

namespace Script
{
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
            Jump();
        }

        /// <summary>
        /// Move the player, play the animation, and flip the sprite accordingly to the direction of the movement.
        /// </summary>
        private void Move()
        {
            Vector2 movement = new Vector2(Input.GetAxis(axisName) * moveSpeed * Time.deltaTime, 0);
            _animator.SetBool(IsWalking, Input.GetAxis(axisName) != 0);
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
            playerRigid.velocity = new Vector2(Mathf.Clamp(playerRigid.velocity.x, -maxMoveVelocity, maxMoveVelocity),
                playerRigid.velocity.y);
            playerRigid.AddForce(movement);
        }

        /// <summary>
        /// Check if the player is on the ground
        /// </summary>
        /// <returns></returns>
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
