using System.Collections;
using UnityEngine;

namespace Script
{
    public class SpikyFootballBehavior : MonoBehaviour
    {
        private GameController _gameController;
        [HideInInspector] public Vector2 velocity;
        [HideInInspector] public new Rigidbody2D rigidbody;
        [HideInInspector] public float destroyDelay;
        [HideInInspector] public bool destroyOnCollision;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            _gameController = FindObjectOfType<GameController>();
        }

        private void Start()
        {
            StartCoroutine(CountDownUntilDestroy());
        }

        private void Update()
        {
            if (velocity.magnitude == 0) return;
            rigidbody.velocity = velocity;
            float rotation = -100 * Time.deltaTime * rigidbody.velocity.magnitude;
            transform.Rotate(0, 0, rotation);
        }

        private IEnumerator CountDownUntilDestroy()
        {
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _gameController.GameOver();
            }
            if (destroyOnCollision)
                Destroy(gameObject);
        }
    }
}
