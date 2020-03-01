using TMPro;
using UnityEngine;

namespace PingPong
{
    public sealed class StartGameUserInterface : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerStartGameLabel;
        private BallManager _ballManager;
        private float _time;
        private bool _startTime;

        private void OnEnable()
        {
            _ballManager = FindObjectOfType<BallManager>();
            _ballManager.StartGameChange += BallManagerOnStartGameChange;
        }

        private void OnDisable()
        {
            _ballManager.StartGameChange -= BallManagerOnStartGameChange;
        }

        private void BallManagerOnStartGameChange(float time)
        {
            _time = time;
            _startTime = true;
            _timerStartGameLabel.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!_startTime)
            {
                return;
            }

            _time -= Time.deltaTime;

            _timerStartGameLabel.text = $"{_time:0}";
            if (_time <= 0.0f)
            {
                _startTime = false;
                _timerStartGameLabel.gameObject.SetActive(false);
            }
        }
    }
}
