using UnityEngine;

namespace Script
{
    public class VictoryLine : MonoBehaviour
    {
        private int _playersInTrigger;
        private GameController _gameController;
        
        private void Awake()
        {
            _gameController = FindObjectOfType<GameController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playersInTrigger++;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playersInTrigger--;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && _playersInTrigger >= 2)
            {
                _gameController.Victory();
            }
        }
    }
}
