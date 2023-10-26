using UnityEngine;
using UnityEngine.UI;

public class SliderEnemyHP : MonoBehaviour
{
    public Slider SliderEnemyHealth;
    [SerializeField] private Image _fillImg;
    private Color _maxColor = Color.green;
    private Color _minColor = Color.red;

   [SerializeField] private EnemyBehavior _enemyBehavior;

    private void Awake()
    {
        SliderEnemyHealth.maxValue = 5;
    }
    
    public void ChangeColorSlidere(int enemyHp)
    {
        SliderEnemyHealth.value = enemyHp;

        _fillImg.color = Color.Lerp(_minColor, _maxColor, (float)enemyHp / _enemyBehavior._maxHP);
    }
}
