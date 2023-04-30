using UnityEngine;
using SPStudios.Tools;

namespace Script
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverCanvas;
        
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1f;
            gameOverCanvas.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GameOver()
        {
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);
        }

        public void Victory()
        {
        
        }
    }
}
