using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject[] _players;
    private GameObject _topMostPlayer;

    public GameObject GameOverCanvas;
    
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float smoothingSpeed = 20f;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    [SerializeField] private float timeCoundown = 2f;

    private bool _isPlayerInViwe = true;
    private float _loseTimer;
    private bool _hasPlayerLost;

    // Start is called before the first frame update
    private void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        _hasPlayerLost = false;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckPlayerPosition();
        SetNewPosition();
        CheckPlayerPosition2();
        CheckPlayerLose();
    }

    /// <summary>
    /// Check which player is the highest and set it as the top most player
    /// </summary>
    private void CheckPlayerPosition()
    {
        //Select the y position of each player and put it in a list
        List<float> yPositions = _players.Select(t => t.transform.position.y).ToList();
        //Get the index of the highest y position and set the top most player
        _topMostPlayer = _players[yPositions.IndexOf(yPositions.Max())];
    }

    /// <summary>
    /// Set the new position of the camera
    /// </summary>
    private void SetNewPosition()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, _topMostPlayer.transform.position,
            curve.Evaluate(Time.deltaTime) * smoothingSpeed);
        float x = Mathf.Clamp(newPosition.x, xMin, xMax);
        float y = Mathf.Clamp(newPosition.y, yMin, yMax);
        //Set the new position of the camera from clamped x and y
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }

    private void CheckPlayerPosition2()
    {
        Camera mainCamera = Camera.main;

        foreach (GameObject player in _players)
        {
            Vector3 screenPos = mainCamera.WorldToViewportPoint(player.transform.position);

            if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1 || screenPos.z < 0)
            {
                _isPlayerInViwe = false;
                
                //Debug.Log("Player Lose");
                break;
            }
            else
            {
                _isPlayerInViwe = true;
            }
        }
    }

    private void CheckPlayerLose()
    {
        if (!_isPlayerInViwe && !_hasPlayerLost)
        {
            _loseTimer += Time.deltaTime;

            if (_loseTimer > timeCoundown)
            {
                _hasPlayerLost = true;
                Time.timeScale = 0f;
                
                GameOverCanvas.SetActive(true);

                Debug.Log("Game Over");
            }
        }
    }

}