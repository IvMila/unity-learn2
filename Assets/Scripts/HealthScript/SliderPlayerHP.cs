using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderPlayerHP : MonoBehaviour
{
    public Slider SliderPlayerHealth;

    private int _targetHealth;
    private float _timeScale = 0;

    private PlayerBehavior _playerBehavior;

    private Color _maxPlayerHpColor = Color.green;
    private Color _minPlayerHpColor = Color.red;
    [SerializeField] private Image _fillImg;

    private void Start()
    {
        _playerBehavior = PlayerBehavior.PlayerBehaviorInstance;
    }
    public void SetHealth(int hp)
    {
        _targetHealth = hp;
        _timeScale = 0;

        StartCoroutine(LerpHealth());
    }

    public void SetColors(int hp)
    {
        SliderPlayerHealth.value = hp;

        _fillImg.color = Color.Lerp(_minPlayerHpColor, _maxPlayerHpColor, (float)hp / _playerBehavior._maxHP);
    }

    private IEnumerator LerpHealth()
    {
        float speedLerp = 5f;
        float startHealth = SliderPlayerHealth.value;

        while (_timeScale < 1)
        {
            _timeScale += Time.deltaTime * speedLerp;
            SliderPlayerHealth.value = Mathf.Lerp(startHealth, _targetHealth, _timeScale);
            yield return null;
        }
    }
    public void OffSlider()
    {
        StopCoroutine(LerpHealth());
    }
}
