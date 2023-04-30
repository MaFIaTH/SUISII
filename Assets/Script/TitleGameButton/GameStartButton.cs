using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    private Button _GameStartButton;
    
    // Start is called before the first frame update
    void Start()
    {
        _GameStartButton = GetComponent<Button>();
        _GameStartButton.onClick.AddListener(GameStart);
    }

    void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
