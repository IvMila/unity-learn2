using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior PlayerBehaviorInstance;

    [HideInInspector] public Animator _characterAnimatorController;

    [Header("Settings References")]
    private readonly float SpeedMove = 5f;
    private readonly float SpeedRotate = 5f;
    private float _hInput;
    private float _vInput;
    private readonly float _jumpVelocity = 3f;
    [HideInInspector] public readonly int _maxHP = 10;
    private int _playerHP;
    [SerializeField] private SliderPlayerHP _sliderPlayerHP;
    public LayerMask GroundLayer;
    private CapsuleCollider _capsuleCollider;
    private readonly float _distanceToGround = 0.1f;
    private Rigidbody _rigidbody;
    private UIBehavior _uIBehavior;
    private int _damage = 4;
    private DamagePopUp _damagePopUp;
    private TestCamBehavior testCamBehavior;
    [SerializeField] private SceneBehavoir sceneBehavoir;
    private void Awake()
    {
        PlayerBehaviorInstance = this;
    }

    private void Start()
    {
        _playerHP = _maxHP;

        _rigidbody = GetComponent<Rigidbody>();

        _capsuleCollider = GetComponent<CapsuleCollider>();

        _uIBehavior = UIBehavior.InstanceUIBehavior;

        _characterAnimatorController = GetComponent<Animator>();

        _damagePopUp = DamagePopUp.InstanceDamage;

        testCamBehavior = TestCamBehavior.ChangeVirtualCameraInstance;
    }

    private void Update()
    {
        _sliderPlayerHP.SetColors(PlayerHealth);

        _hInput = Input.GetAxis("Horizontal") * SpeedRotate;
        _vInput = Input.GetAxis("Vertical") * SpeedMove;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _uIBehavior.ActiveInventory();
        }
    }

    private void FixedUpdate()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * _jumpVelocity, ForceMode.Impulse);
            _characterAnimatorController.SetBool("Jump", true);
        }
        else _characterAnimatorController.SetBool("Jump", false);

        //Vector3 rotation = Vector3.up * _hInput;
        //Quaternion angleRotate = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _characterAnimatorController.SetFloat("Vertical", _vInput);
        _rigidbody.MovePosition(transform.position + transform.forward * _vInput * Time.fixedDeltaTime);

        _characterAnimatorController.SetFloat("Horizontal", _hInput);
        transform.position = transform.position + transform.right * _hInput * Time.fixedDeltaTime;

        //_rigidbody.MoveRotation(_rigidbody.rotation * angleRotate);
        //_rigidbody.MovePosition(transform.position + transform.right * _hInput * Time.fixedDeltaTime);
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_capsuleCollider.bounds.center.x,
            _capsuleCollider.bounds.min.y, _capsuleCollider.bounds.center.z);
        bool checkGround = Physics.CheckCapsule(_capsuleCollider.bounds.center,
            capsuleBottom, _distanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);
        return checkGround;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {
            PlayerHealth -= _damage;
            _sliderPlayerHP.SetHealth(PlayerHealth);
            _damagePopUp.CreatePopUp("-" + _damage.ToString());
        }
    }

    private void DeathPlayer()
    {
        _characterAnimatorController.SetBool("Dead", true);
        testCamBehavior.DeathCameraChange();
        _uIBehavior.ScreenLoss();
        _sliderPlayerHP.OffSlider();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _rigidbody.useGravity = false;
        _capsuleCollider.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        sceneBehavoir.StartCoroutineLevel();
    }

    public void EndAnimation()
    {
        _characterAnimatorController.SetFloat("Vertical", 0);
        _characterAnimatorController.SetFloat("Horizontal", 0);
    }

    public int PlayerHealth
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                DeathPlayer();
            }
            if (_playerHP <= 5)
            {
                _uIBehavior.WarringHealth();
            }
            else _uIBehavior.OffWarringHealth();
        }
    }
}
