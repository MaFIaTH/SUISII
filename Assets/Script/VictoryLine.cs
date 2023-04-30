using UnityEngine;

namespace Script
{
    public class VictoryLine : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        
        private int _playersInTrigger = 0;

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
                gameController.Victory();
            }
        }
    }
}
