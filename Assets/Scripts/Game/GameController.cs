using System.Collections.Generic;
using Help;
using Input;
using UI;
using UnityEngine;

namespace Game
{
    public class GameController : Singleton<GameController>
    {
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private List<Vector3> _targetsPopsitions;
        [SerializeField] private List<Vector3> _spawnPopsitions;

        [SerializeField] private TriggerCollisionDetector _target;

        private Vector3 _targetPosition;
        private Timer _timer;

        private void Start()
        {
            GameState.Reset();

            this._inputListener.OnHit += OnHit;

            //choose the random preset index
            int i = Random.Range(0, this._targetsPopsitions.Count);

            //init players and enemies
            PlayerController.Instance.Init(10, this._spawnPopsitions[i], 5);

            EnemyController.Instance.Init(7, 20);
            EnemyController.Instance.OnAllPlayersKilled += GameFailed;

            InitTargetPosition(i);

            //start the timer
            this._timer = Instantiate(new GameObject()).AddComponent<Timer>();
            _timer.InitTimer(60, OnTimeChanged, OnTimerFinish);
        }

        /// <summary>
        /// Move the target to preset position by index
        /// </summary>
        /// <param name="i"> preset position index</param>
        private void InitTargetPosition(int i)
        {
            this._targetPosition = this._targetsPopsitions[i];
            this._target.transform.position = this._targetPosition;

            this._target.OnTrigger += OnTargetCollied;
        }

        /// <summary>
        /// Invokes when target object was collied with something
        /// </summary>
        /// <param name="target">target</param>
        /// <param name="collider">collider collied with target</param>
        private void OnTargetCollied(TriggerCollisionDetector target, Collider collider)
        {
            //if players was collied with target
            if (collider.tag == "Player")
            {
                //game win
                GameUI.Instance.GameWin();
                EnemyController.Instance.KillAllEnemies();
                Finish();
                StopAllCoroutines();
            }
        }

        private void Finish()
        {
            this._timer.Stop();
            GameState.Finishe();
        }

        /// <summary>
        /// Invokes when time left changed
        /// </summary>
        /// <param name="timerLeft"> current time left</param>
        private void OnTimeChanged(int timerLeft)
        {
            GameUI.Instance.SetTimerLeft(timerLeft);
        }

        /// <summary>
        /// Invokes when timer finished and game over
        /// </summary>
        private void OnTimerFinish()
        {
            GameFailed();
        }

        /// <summary>
        /// Set game as failed
        /// </summary>
        private void GameFailed()
        {
            GameUI.Instance.GameFail();
            Finish();
        }

        /// <summary>
        /// Invokes when was handled click on the screen
        /// </summary>
        /// <param name="hit">hit info</param>
        private void OnHit(RaycastHit hit)
        {
            if (!GameState.Paused && !GameState.HasFinished)
            {
                //if enemy wasn't taped
                if (hit.collider.tag != "Enemy")
                {
                    //set for players new position to follow
                    PlayerController.Instance.SetTarget(hit.point);
                }
            }
        }
    }
}