using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script
{
    public class RestartGameButton : MonoBehaviour
    {
        private Button _restartButton;

        // Start is called before the first frame update
        void Start()
        {
            _restartButton = GetComponent<Button>();
            _restartButton.onClick.AddListener(RestartGame);
        }

        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
