using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class Coin : MonoBehaviour
    {
        private CurrencyManager _currencyManager;
        private PoolingSystem _poolingSystem;
        private SfxManager _sfxManager;
        [SerializeField] private int coinValue;

        private void Awake()
        {
            _currencyManager = FindAnyObjectByType<CurrencyManager>();
            _poolingSystem = FindAnyObjectByType<PoolingSystem>();
            _sfxManager = FindAnyObjectByType<SfxManager>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != 6) return;
            _currencyManager.AddCurrency(coinValue);
            _sfxManager.PlaySfx(SfxManager.Sounds.Gem);
            _poolingSystem.ReturnToPool(gameObject);
        }
    }
}
