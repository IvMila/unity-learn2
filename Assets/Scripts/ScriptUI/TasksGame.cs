using UnityEngine;
using TMPro;

public class TasksGame : MonoBehaviour
{
    public static TasksGame InstanceTask;
    [Header("Prefab Refarences")]
    public TextMeshProUGUI _taskText;

    [HideInInspector] public int MaxItems = 4;

    [HideInInspector] public int ItemCollected = 0;

    private void Awake()
    {
        InstanceTask = this;
    }

    public void Item()
    {
        if (MaxItems <= 0)
        {
            _taskText.gameObject.SetActive(false);
        }
        else { _taskText.text = "Your task now is to collect items: " + MaxItems + " and killed enemy"; }
    }
}
