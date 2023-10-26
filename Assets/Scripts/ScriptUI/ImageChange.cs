using UnityEngine;

public class ImageChange : MonoBehaviour
{
    [SerializeField] private Sprite _img1;
    [SerializeField] private Sprite _img2;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _img1;
    }

    public void Change()
    {
        if(_spriteRenderer.sprite == _img1)
        {
            _spriteRenderer.sprite = _img2;
            return;
        }
    }
}
