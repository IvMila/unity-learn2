using UnityEngine;

public class LookAtPlayerHP : MonoBehaviour
{
    public Transform Camera;

    void LateUpdate()
    {
        transform.LookAt(Camera);
    }
}
