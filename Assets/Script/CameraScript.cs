using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script
{
    public class CameraScript : MonoBehaviour
    {
        private GameObject[] _players;
        private GameObject _topMostPlayer;

        [SerializeField] private GameController gameController;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float smoothingSpeed = 20f;
        [SerializeField] private float xMin;
        [SerializeField] private float xMax;
        [SerializeField] private float yMin;
        [SerializeField] private float yMax;
        
        private Camera _mainCamera;

        // Start is called before the first frame update
        private void Start()
        {
            Time.timeScale = 1f;
            _mainCamera = Camera.main;
            _players = GameObject.FindGameObjectsWithTag("Player");
        }

        // Update is called once per frame
        private void Update()
        {
            CheckPlayerPosition();
            SetNewPosition();
            CheckIfPlayerIsInCamera();
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
            var o = gameObject;
            o.transform.position = new Vector3(x, y, o.transform.position.z);
        }

        private void CheckIfPlayerIsInCamera()
        {
            Camera mainCamera = _mainCamera;

            foreach (GameObject player in _players)
            {
                Vector3 screenPos = mainCamera.WorldToViewportPoint(player.transform.position);

                if (screenPos.x is < 0 or > 1 || screenPos.y is < 0 or > 1 || screenPos.z < 0)
                {
                    gameController.GameOver();
                }
            }
        }
    }
}