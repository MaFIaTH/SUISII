using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    private Button _QuitGameButton;
    // Start is called before the first frame update
    void Start()
    {
        _QuitGameButton = GetComponent<Button>();
        _QuitGameButton.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
