using UnityEngine.UI;
using UnityEngine;

public class SliderStatisticView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void Display(float maxHealth, float currentHealth)
    {
        float progress = currentHealth / maxHealth;
        _slider.value = progress;
    }
}
