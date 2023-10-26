using UnityEngine;
using Cinemachine;

public class ChangeVirtualCamera : MonoBehaviour
{
    //public static ChangeVirtualCamera ChangeVirtualCameraInstance;

    //[SerializeField] private CinemachineVirtualCamera[] _virtualCamera;
    //private int _currentIndexCamera;

    //private float _camSens = 4f;
    //private float _xRotation = 0f;
    //[SerializeField] private Transform _player;
    //private UIBehavior uIBehavior;
    //[HideInInspector] public Ray ray;

    //private void Awake()
    //{
    //    ChangeVirtualCameraInstance = this;
    //}

    //private void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    uIBehavior = UIBehavior.InstanceUIBehavior;
    //}

    //private void Update()
    //{
    //    CameraMoveWithCamera();
    //    Vector3 point = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //    ray.origin = point;
    //    ray.direction = transform.forward;
    //    Debug.DrawRay(point, transform.forward * 4f, Color.red);

    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        if (hit.collider.CompareTag("Box"))
    //        {
    //            uIBehavior.VisibleBoxLetter_Key();
    //        }
    //        else uIBehavior.InvisibleBoxLettert_Key();
    //    }
    //}

    //public void DeathCameraChange()
    //{
    //    _virtualCamera[_currentIndexCamera].Priority = 0;
    //    _currentIndexCamera++;
    //        if(_currentIndexCamera>=_virtualCamera.Length)
    //        _currentIndexCamera = 0;
    //    _virtualCamera[_currentIndexCamera].Priority = 1;
    //}

    //private void CameraMoveWithCamera()
    //{
    //    float mousePosX = Input.GetAxis("Mouse X") * _camSens;
    //    float mousePosY = Input.GetAxis("Mouse Y") * _camSens;

    //    _xRotation -= mousePosY;//считывание координат угла наклона камеры по вертикали
    //    _xRotation = Mathf.Clamp(_xRotation, -30, 30);
    //    transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    //    _player.Rotate(Vector3.up, mousePosX);
    //}
}
