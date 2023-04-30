using System;
using System.Collections;
using UnityEngine;

namespace Script
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] private float upTime;
        [SerializeField] private float downTime;
        [SerializeField] private float animationSpeed = 1f;
        private Animator _animator;
        private GameController _gameController;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _gameController = FindObjectOfType<GameController>();
        }

        private void Start()
        {
            StartCoroutine(SpikeBehavior());
        }

        private IEnumerator SpikeBehavior()
        {
            while (true)
            {
                _animator.speed = animationSpeed;
                _animator.Play("SpikeUp");
                yield return new WaitForSeconds(upTime);
                _animator.speed = animationSpeed;
                _animator.Play("SpikeDown");
                yield return new WaitForSeconds(downTime);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _gameController.GameOver();
            }
        }
    }
}
