using System.Collections;
using UnityEngine;
using Cinemachine;

public class TestCamBehavior : MonoBehaviour
{
    private Vector3 startPosition;
    private bool _zoom;
    private bool _zoomStart;

    public static TestCamBehavior ChangeVirtualCameraInstance;

    public CinemachineVirtualCamera[] _virtualCamera;
    private int _currentIndexCamera;

    private UIBehavior uIBehavior;
    [HideInInspector] public Ray rayCam;

    //[SerializeField] private GameObject _aimPointPrefab;
    //private GameObject newAimPoint;
    private void Awake()
    {
        ChangeVirtualCameraInstance = this;
    }

    private void Start()
    {
        startPosition = new Vector3(0.5f, 2f, -1f);
        _virtualCamera[_currentIndexCamera].transform.localPosition = startPosition;
        Cursor.lockState = CursorLockMode.Locked;
        uIBehavior = UIBehavior.InstanceUIBehavior;

        //newAimPoint = Instantiate(_aimPointPrefab, transform);
    }

    private void Update()
    {
        ChangeCameraZoom();

        Vector3 point = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rayCam.origin = point;
        rayCam.direction = transform.forward;
        Debug.DrawRay(point, transform.forward * 4f, Color.red);

        if (Physics.Raycast(rayCam, out RaycastHit hit))
        {
            //Vector3 imgPos = hit.point + hit.normal * 0.1f;

            //newAimPoint.transform.position = imgPos;
            //newAimPoint.transform.LookAt(Camera.main.transform);

            if (hit.collider.CompareTag("Box"))
            {
                uIBehavior.VisibleBoxLetter_Key();
            }
            else uIBehavior.InvisibleBoxLettert_Key();
        }
    }
    public void DeathCameraChange()
    {
        _virtualCamera[_currentIndexCamera].Priority = 0;
        _currentIndexCamera++;
        if (_currentIndexCamera >= _virtualCamera.Length)
            _currentIndexCamera = 0;
        _virtualCamera[_currentIndexCamera].Priority = 1;
    }

    private void ChangeCameraZoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _zoom = true;

            if (!_zoomStart)
            {
                _zoomStart = true;

                Vector3 move = new Vector3(-0.18f, 1.7f, 0.35f);
                _virtualCamera[_currentIndexCamera].transform.localPosition = move;
            }
            else if(_zoom)
            {
                _zoomStart = false;
                _zoom = false;

                _virtualCamera[_currentIndexCamera].transform.localPosition = startPosition;
            }
        }
    }
}
