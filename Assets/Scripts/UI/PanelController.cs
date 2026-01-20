using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] protected Button _hideButton;

    private void Awake()
    {
        _hideButton.onClick.AddListener(HidePanel);
    }

    public virtual void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
