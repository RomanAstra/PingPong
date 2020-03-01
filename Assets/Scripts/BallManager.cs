using System;
using System.Collections;
using UnityEngine;

namespace PingPong
{
    public sealed class BallManager : MonoBehaviour
    {
        public event Action<float> StartGameChange = delegate(float time) {  };
        public event Action<PlayerType> PlayerWinChange = delegate(PlayerType type) {  }; 
        [SerializeField] private SettingsBall _settingsBall;
        private Camera _cameraMain;
        private float _delayBeforeGame = 0.5f;
        private Ball _ball;

        private void Start()
        {
            StartCoroutine(Reset());
        }

        private void OnEnable()
        {
            _cameraMain = Camera.main;
            _ball = FindObjectOfType<Ball>();
            _ball.InvisibleChange += BallOnInvisibleChange;
        }

        private void OnDisable()
        {
            _ball.InvisibleChange -= BallOnInvisibleChange;
        }

        private void BallOnInvisibleChange(PlayerType obj)
        {
            PlayerWinChange.Invoke(obj);
            StartCoroutine(Reset());
        }

        private IEnumerator Reset()
        {
            var centerScreen =  _cameraMain.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            _ball.transform.position = new Vector2(centerScreen.x, centerScreen.y);
            _ball.Rigidbody2D.Sleep();

            var scaleRandom =_settingsBall.GetScaleRandom();
            _ball.transform.localScale = new Vector3(scaleRandom, scaleRandom, 0.0f);
                   
            _ball.Material.color = _settingsBall.GetColorRandom();
            StartGameChange.Invoke(_settingsBall.TimeWaitForStart);
            yield return new WaitForSeconds(_settingsBall.TimeWaitForStart + _delayBeforeGame);

            var velocityXRandom = _settingsBall.GetVelocityXRandom();
            var velocityYRandom = _settingsBall.GetVelocityYRandom();
            _ball.Rigidbody2D.velocity = new Vector2(velocityXRandom, velocityYRandom);
            
            yield break;
        }
    }
}
