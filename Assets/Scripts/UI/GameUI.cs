using Game;
using Help;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUI : Singleton<GameUI>
    {
        [SerializeField] private Button _pause;
        [SerializeField] private Button _restart;

        [SerializeField] private GameObject _gameFailObject;
        [SerializeField] private GameObject _gameWinObject;

        [SerializeField] private TextMeshProUGUI _timeLeft;

        private bool _paused;

        private void Start()
        {
            this._pause.onClick.AddListener(PauseClicked);
            this._restart.onClick.AddListener(RestartClicked);
        }

        public void SetTimerLeft(int timerLeft)
        {
            _timeLeft.text = timerLeft.ToString();
        }

        public void GameFail()
        {
            this._gameFailObject.SetActive(true);
        }

        public void GameWin()
        {
            _gameWinObject.SetActive(true);
        }

        private void RestartClicked()
        {
            LoadLevelHelper.LoadGame();
        }

        private void PauseClicked()
        {
            if (this._paused)
            {
                GameController.Instance.Unpause();
            }
            else
            {
                GameController.Instance.Pause();
            }

            this._paused = !this._paused;
        }
    }
}