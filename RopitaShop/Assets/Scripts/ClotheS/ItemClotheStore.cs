using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemClotheStore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemCostTxt;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _cost;
    [SerializeField] private GameObject _cantAffordImg;
    private ClotheType _clotheType;
    private string _itemID;
    private int _itemPrice;
    private int _itemIndex;
    private Color _color = Color.white;
    private bool _isPurchased = false;

    private void Start()
    {
        if (_button == null) _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
        _isPurchased = false;
        
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnCurrencyChanged += HandleCurrencyChanged;
        }
    }

    private void OnDestroy()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnCurrencyChanged -= HandleCurrencyChanged;
        }
    }

    public void Initialize(string itemID, Sprite itemSprite, int itemPrice, ClotheType clotheType, int itemIndex)
    {
        _itemID = itemID;
        _icon.sprite = itemSprite;
        _itemPrice = itemPrice;
        _itemCostTxt.text = itemPrice.ToString();
        _clotheType = clotheType;
        _itemIndex = itemIndex;
        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        if (_button == null) _button = GetComponent<Button>();

        var buttonAnim = GetComponent<ButtonOptimizedAnims>();
        if (buttonAnim != null)
        {
            buttonAnim.ResetNormalColor(Color.white);
        }

        if (!_isPurchased)
        {
            bool canAfford = CurrencyManager.Instance.CanAfford(_itemPrice);
            _button.interactable = canAfford;
            _cantAffordImg.SetActive(!canAfford);
            _cost.SetActive(true);
        }
        else if (_isPurchased)
        {
            _button.interactable = true;
            _cost.SetActive(false);
            _cantAffordImg.SetActive(false);
        }
    }

    private void HandleCurrencyChanged(int newCurrency)
    {
        UpdateButtonState(); 
    }

    private void OnButtonClicked()
    {
        Debug.Log("Clicked");

        if (!_isPurchased && CurrencyManager.Instance.CanAfford(_itemPrice))
        {
            CurrencyManager.Instance.SpendCurrency(_itemPrice);
            _isPurchased = true;
            UpdateButtonState();
            CharacterPartSetector.Instance.ChangeBodyPart(_clotheType.ToString(), _itemIndex);
        }
        else if (!_isPurchased)
        {
            Debug.Log("nel");
        }
        else
        {
            CharacterPartSetector.Instance.ChangeBodyPart(_clotheType.ToString(), _itemIndex);
        }
    }

    public bool CanAfford()
    {
        return CurrencyManager.Instance.CanAfford(_itemPrice);
    }
    
}
