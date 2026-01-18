using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TextStatisticView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _maxStateColor = Color.green;
    [SerializeField] private Color _midStateColor = Color.yellow;
    [SerializeField] private Color _criticalStateColor = Color.red;

    public void Display(float maxValue, float currentValue)
    {
        _text.text = $"{currentValue}";
        ApplyColor(maxValue, currentValue);
    }

    private void ApplyColor(float maxValue, float currentValue)
    {
        if (currentValue > maxValue * (2f / 3f))
        {
            _text.color = _maxStateColor;
        }
        else if (currentValue > maxValue / 3f)
        {
            _text.color = _midStateColor;
        }
        else
        {
            _text.color = _criticalStateColor;
        }
    }
}
