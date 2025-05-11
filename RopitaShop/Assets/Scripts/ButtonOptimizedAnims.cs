using UnityEngine;
using UnityEngine.UI;

public class ButtonOptimizedAnims : Button
{

    [SerializeField] private Image[] _targetImages;
    [SerializeField] private Color[] _normalColors = null;
    [SerializeField] private Color[] _disabledColors = null;
    public  Color normalColor = Color.white;
    private static Color defaultDisabledColor = new Color(0.78f, 0.78f, 0.78f, 0.5f);
    private static Vector3 disabledScale = Vector3.one;
    private static Vector3 highlightedScale = Vector3.one;
    private static Vector3 normalScale = Vector3.one;
    private static Vector3 pressedScale = new Vector3(0.95f, 0.95f, 1f);
    private static Vector3 selectedScale = Vector3.one;
      
    public void GetTargetColors()
    {
        _normalColors = new Color[targetImages.Length];
        _disabledColors = new Color[targetImages.Length];
        
        for (int i = 0; i < targetImages.Length; i++)
        {
            _normalColors[i] = targetImages[i].color;
            _disabledColors[i] = defaultDisabledColor;
        }
            
        normalColor = _normalColors[0];
    }
        
    public void ResetNormalColor(Color color)
    {
        normalColor = color;
        _normalColors = new Color[targetImages.Length];
        _disabledColors = new Color[targetImages.Length];
            
        for (int i = 0; i < targetImages.Length; i++)
        {
            _normalColors[i] = color;
            _disabledColors[i] = defaultDisabledColor;
        }
    }

    public Image[] targetImages
    {
        get
        {
            if (_targetImages == null)
            {
                _targetImages = targetGraphic.GetComponentsInChildren<Image>(true);
            }

            return _targetImages;
        }
    }

    public Color[] normalColors
    {
        get
        {
            if (_normalColors == null || _normalColors.Length == 0) GetTargetColors();
            return _normalColors;
        }
    }

    public Color[] disabledColors
    {
        get
        {
            if (_disabledColors == null || _disabledColors.Length == 0) 
                GetTargetColors();
            return _disabledColors;
        }
    }
    
    protected override void DoStateTransition(SelectionState state, bool instant)
    { 
        if (!gameObject.activeInHierarchy) return;

        Vector3 targetScale = Vector3.one;
        switch (state)
        {
            case SelectionState.Normal: targetScale = normalScale;
                for (int i = 0; i < targetImages.Length; i++)
                {
                    targetImages[i].color = normalColors[i];
                }
                break;
            case SelectionState.Highlighted:
                targetScale = highlightedScale;
                break;
            case SelectionState.Pressed:
                targetScale = pressedScale;
                break;
            case SelectionState.Selected:
                targetScale = selectedScale;
                break;
            case SelectionState.Disabled:
                targetScale = disabledScale;
                
                for (int i = 0; i < targetImages.Length; i++)
                { 
                    targetImages[i].color = disabledColors[i];
                }

                break;
            default:
                targetScale = Vector3.zero;
                break;
        }
        transform.localScale = targetScale;
    }
    
}
