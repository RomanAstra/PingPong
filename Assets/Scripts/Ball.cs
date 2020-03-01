using System;
using System.Collections;
using UnityEngine;

namespace PingPong
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Ball : MonoBehaviour
    {
        public event Action<PlayerType> InvisibleChange = delegate(PlayerType type) {  };
        public Rigidbody2D Rigidbody2D { get; private set; }
        public Material Material { get; private set; }
        private float _delayBeforeGame = 0.5f;
        
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Material = GetComponent<Renderer>().material;
        }

        private void OnBecameInvisible()
        {
            InvisibleChange.Invoke(transform.localPosition.y > 0.0f ? PlayerType.DownPlayer : PlayerType.UpPlayer);
        }
    }
}
