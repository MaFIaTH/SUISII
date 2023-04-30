using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private GameObject victoryCanvas;
        [SerializeField] private List<Button> restartButton;
        [SerializeField] private List<Button> mainMenuButton;
        
        private void Awake()
        {
            restartButton.ForEach(button => button.onClick.AddListener(RestartGame));
            mainMenuButton.ForEach(button => button.onClick.AddListener(MainMenu));
        }

        private void Start()
        {
            Time.timeScale = 1f;
            gameOverCanvas.SetActive(false);
        }
        
        public void GameOver()
        {
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);
        }

        public void Victory()
        {
            Time.timeScale = 0f;
            victoryCanvas.SetActive(true);
        }
        
        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
