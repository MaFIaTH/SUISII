using UnityEngine;
using UnityEngine.UI;

namespace Script.TitleGameButton
{
    public class QuitGameButton : MonoBehaviour
    {
        private Button _quitGameButton;
        // Start is called before the first frame update
        private void Start()
        {
            _quitGameButton = GetComponent<Button>();
            _quitGameButton.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
