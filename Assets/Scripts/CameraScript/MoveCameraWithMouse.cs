using UnityEngine;

public class MoveCameraWithMouse : MonoBehaviour
{
    public static MoveCameraWithMouse MoveCameraInstance;
    private float _camSens = 4f;
    private float _xRotation = 0f;
    [SerializeField] private Transform _player;

    private void Awake()
    {
        MoveCameraInstance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CameraMoveWithMouse()
    {
        float mousePosX = Input.GetAxis("Mouse X") * _camSens;
        float mousePosY = Input.GetAxis("Mouse Y") * _camSens;

        _xRotation -= mousePosY;//считывание координат угла наклона камеры по вертикали
        _xRotation = Mathf.Clamp(_xRotation, -30, 30);
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _player.Rotate(Vector3.up, mousePosX);
    }

    private void Update()
    {
        CameraMoveWithMouse();
    }
}

