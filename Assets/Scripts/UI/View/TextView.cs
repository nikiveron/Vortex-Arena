using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Display(int currentValue)
    {
        _text.text = $"{currentValue}";
    }
}
