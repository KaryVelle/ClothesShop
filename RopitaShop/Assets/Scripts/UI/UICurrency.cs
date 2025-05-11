using Managers;
using UnityEngine;

public class UICurrency : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _currencyText;
    void Start()
    {
        UpdateCurrencyText(CurrencyManager.Instance.Currency);
        CurrencyManager.Instance.OnCurrencyChanged += UpdateCurrencyText; 
    }

    private void UpdateCurrencyText(int currency)
    {
        _currencyText.text = currency.ToString();
    }
}
