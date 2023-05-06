using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script
{
    public class CameraScript : MonoBehaviour
    {
        private GameObject[] _players;
        private GameObject _targetPlayer;

        [SerializeField] private GameController gameController;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float smoothingSpeed = 20f;
        [SerializeField] private float yDifferentThreshold = 5f;
        [SerializeField] private float xDifferentThreshold = 5f;
        [SerializeField] private float xMin;
        [SerializeField] private float xMax;
        [SerializeField] private float yMin;
        [SerializeField] private float yMax;
        
        private Camera _mainCamera;
        private List<BoxCollider2D> _colliders;

        private void Start()
        {
            Time.timeScale = 1f;
            _mainCamera = Camera.main;
            _players = GameObject.FindGameObjectsWithTag("Player");
            _colliders = _players.Select(player => player.GetComponent<BoxCollider2D>()).ToList();
        }
        
        private void Update()
        {
            CheckPlayerPosition();
            SetNewPosition();
            CheckIfPlayerIsInCamera();
        }

        /// <summary>
        /// Check which player is the target player for camera to follow
        /// </summary>
        private void CheckPlayerPosition()
        {
            //Select the y position of each player and put it in a list
            List<float> yPositions = _players.Select(t => t.transform.position.y).ToList();
            if (Mathf.Abs(yPositions[0] - yPositions[1]) >= yDifferentThreshold)
            {
                //Get the index of the highest y position and set the top most player
                _targetPlayer = _players[yPositions.IndexOf(yPositions.Max())];
                return;
            }
            List<float> xPositions = _players.Select(t => t.transform.position.x).ToList();
            if (Mathf.Abs(xPositions[0] - xPositions[1]) >= xDifferentThreshold)
            {
                int index = Mathf.Abs(xPositions[0]) > Mathf.Abs(xPositions[1]) ? 0 : 1;
                _targetPlayer = _players[index];
                return;
            }
            _targetPlayer = _players[0];
        }

        /// <summary>
        /// Set the new position of the camera
        /// </summary>
        private void SetNewPosition()
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, _targetPlayer.transform.position,
                curve.Evaluate(Time.deltaTime) * smoothingSpeed);
            float x = Mathf.Clamp(newPosition.x, xMin, xMax);
            float y = Mathf.Clamp(newPosition.y, yMin, yMax);
            //Set the new position of the camera from clamped x and y
            var o = gameObject;
            o.transform.position = new Vector3(x, y, o.transform.position.z);
        }

        /// <summary>
        /// Check if the player is in the camera field of view. If not, game over
        /// </summary>
        private void CheckIfPlayerIsInCamera()
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);
            foreach (BoxCollider2D _ in _colliders.Where(boxCollider2D =>
                         !GeometryUtility.TestPlanesAABB(planes, boxCollider2D.bounds)))
            {
                gameController.GameOver();
            }
        }
    }
}