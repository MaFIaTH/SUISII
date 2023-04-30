using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private bool useOffset;
        
        [ShowIf("useOffset")]
        [SerializeField] private Vector2 spawnOffset;
        
        [SerializeField] private bool useGravity;
        [SerializeField] private bool useVelocity;
        
        [ShowIf("useVelocity")] 
        [SerializeField] private Vector2 velocity;
        
        [SerializeField] private float spawnInterval;
        [SerializeField] private float destroyDelay = 5f;
        [SerializeField] private bool destroyOnCollision;
        
        private Transform _ballSpawnPoint;
        // Start is called before the first frame update
        private void Start()
        {
            _ballSpawnPoint = transform;
            StartCoroutine(SpawnBall());
        }
        
        private IEnumerator SpawnBall()
        {
            while (true)
            {
                if (useOffset)
                {
                    _ballSpawnPoint.position += (Vector3) spawnOffset;
                }
                SpikyFootballBehavior ball = Instantiate(ballPrefab, _ballSpawnPoint.position, Quaternion.identity)
                    .GetComponent<SpikyFootballBehavior>();
                ball.rigidbody.gravityScale = useGravity ? 1f : 0f;
                ball.destroyDelay = destroyDelay;
                ball.destroyOnCollision = destroyOnCollision;
                if (useVelocity)
                {
                    ball.velocity = velocity;
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
