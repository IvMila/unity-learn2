using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp InstanceDamage;

    [SerializeField] private GameObject _text, _saveText;

    private void Awake()
    {
        InstanceDamage = this;
    }

    public void CreatePopUp(string text)
    {
        GameObject damageText = Instantiate(_text, _saveText.transform);
        damageText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(text);
    }
}
