using UnityEngine;

public class AnimationOpeningDoor : MonoBehaviour
{
    [SerializeField] private Animation _animationOpen;

    private void Start()
    {
        _animationOpen = GetComponent<Animation>();
        GlobalEventManager.OpeningDoor += PlayAnimationDoor;
    }
    public void PlayAnimationDoor()
    {
        _animationOpen.Play(PlayMode.StopAll);
    }
}
