using UnityEngine;

public class AnimationOpeningBox : MonoBehaviour
{
    private Animator _animatorOpeningBox;
    [SerializeField] private AnimationItemPopUp _animationPopUp;
    [SerializeField] private GameObject _untaggedBox;
   
    private void Start()
    {
        _animatorOpeningBox = GetComponent<Animator>();
    }

    public void OpeningBox()
    {
        _animatorOpeningBox.SetTrigger("isOpening");
        _animationPopUp.PlayAnimationItemPopUp();
        _untaggedBox.tag = "Untagged";
    }   
}
