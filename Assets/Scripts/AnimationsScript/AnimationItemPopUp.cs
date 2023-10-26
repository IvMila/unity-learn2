using UnityEngine;

public class AnimationItemPopUp : MonoBehaviour
{
    [SerializeField] private  Animation _animationItemPopUp;

    void Start()
    {
        _animationItemPopUp = GetComponent<Animation>();
    }

    public void PlayAnimationItemPopUp()
    {
        _animationItemPopUp.Play(PlayMode.StopAll);
    }
}
