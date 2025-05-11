using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private CurrencyManager _currencyManager;
        [SerializeField] private PoolingSystem _poolingSystem;
        [SerializeField] private int coinValue;
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != 6) return;
            _currencyManager.AddCurrency(coinValue);
            Debug.Log("Added");
            _poolingSystem.ReturnToPool(gameObject);
        }
    }
}
