using UnityEngine;

public class TaskItem : MonoBehaviour
{
    [SerializeField] private TasksGame _tasksGame;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerBehavior>())
        {
            _tasksGame.ItemCollected++;
            _tasksGame.MaxItems--;
        }
    }
}
