using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PingPong
{
    public sealed class GameUserInterface : MonoBehaviour
    {
        private const string BEST_SCORE = "BEST_SCORE";
        [SerializeField] private GameObject _menuPauseRoot;
        [SerializeField] private TextMeshProUGUI _countPointUpPlayerLabel;
        [SerializeField] private TextMeshProUGUI _countPointDownPlayerLabel;
        [SerializeField] private TextMeshProUGUI _countPointBestPlayerLabel;
        [SerializeField] private Button _showPauseButton;
        [SerializeField] private Button _hidePauseButton;
        private int _countPointUpPlayer;
        private int _countPointDownPlayer;
        private BallManager _ballManager;

        private void OnEnable()
        {
            _ballManager = FindObjectOfType<BallManager>();
            _ballManager.PlayerWinChange += OnPlayerWinChange;
            _showPauseButton.onClick.AddListener(ShowPauseButtonOnClick);
            _hidePauseButton.onClick.AddListener(HidePauseButtonOnClick);
            HidePauseButtonOnClick();
        }

        private void OnDisable()
        {
            CountingBestScore();
            _ballManager.PlayerWinChange -= OnPlayerWinChange;
            _showPauseButton.onClick.RemoveListener(ShowPauseButtonOnClick);
            _hidePauseButton.onClick.RemoveListener(HidePauseButtonOnClick);
        }

        private void ShowPauseButtonOnClick()
        {
            CountingBestScore();
            _menuPauseRoot.SetActive(true);
            _countPointUpPlayerLabel.text = $"Up score = {_countPointUpPlayer}";
            _countPointDownPlayerLabel.text = $"Down score = {_countPointDownPlayer}";
            _countPointBestPlayerLabel.text = $"Beast score = {PlayerPrefs.GetInt(BEST_SCORE)}";
            Time.timeScale = 0.0f;
        }

        private void HidePauseButtonOnClick()
        {
            _menuPauseRoot.SetActive(false);
            Time.timeScale = 1.0f;
        }

        private void OnPlayerWinChange(PlayerType obj)
        {
            switch (obj)
            {
                case PlayerType.UpPlayer:
                    ++_countPointUpPlayer;
                    break;
                case PlayerType.DownPlayer:
                    ++_countPointDownPlayer;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }

        private void CountingBestScore()
        {
            var bestScore = PlayerPrefs.GetInt(BEST_SCORE);
            var bestScoreUser = _countPointUpPlayer > _countPointDownPlayer ? _countPointUpPlayer : _countPointDownPlayer;

            if (bestScore < bestScoreUser)
            {
                PlayerPrefs.SetInt(BEST_SCORE, bestScoreUser);
            }
        }
    }
}
