using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TextStatisticView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Display(float maxValue, float currentValue)
    {
        _text.text = $"{currentValue}/{maxValue}";
    }
}
