using UnityEngine;
using TMPro;
using System.Collections;

public class ObjectDamageText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Awake()
    {
        _text.gameObject.SetActive(false);
    }

    public void Display(float damageValue)
    {
        _text.text = $"{damageValue}";
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        _text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _text.gameObject.SetActive(false);
    }
}
