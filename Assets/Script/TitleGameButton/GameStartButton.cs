using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.TitleGameButton
{
    public class GameStartButton : MonoBehaviour
    {
        private Button _gameStartButton;
    
        // Start is called before the first frame update
        private void Start()
        {
            _gameStartButton = GetComponent<Button>();
            _gameStartButton.onClick.AddListener(GameStart);
        }

        private void GameStart()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
