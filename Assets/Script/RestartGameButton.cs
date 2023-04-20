using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
