using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Prefab References")]
    [SerializeField] private SliderEnemyHP _sliderEnemyHealth;
    public ImageChange _imgChange;
    [SerializeField] private GameObject _key;

    [Header("Location References")]
    [SerializeField] private Transform _patrolRoute;
    [SerializeField] private List<Transform> _locations;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _keyTransform;
    private NavMeshAgent _navMeshAgent;
    private Animator _enemyAnimator;

    [Header("Settings References")]
    private int _indexLocation = 0;
    [HideInInspector] public readonly int _maxHP = 5;
    private int _enemyHP;
    [HideInInspector] public Ray _ray;
    private UIBehavior _uIBehavior;
    [SerializeField] private EnemyShoot _enemyShoot;
    private int _damage = 1;

    private void Start()
    {
        _uIBehavior = UIBehavior.InstanceUIBehavior;

        _enemyAnimator = GetComponent<Animator>();
        InitializePatrolRoute();

        _enemyHP = _maxHP;

        _navMeshAgent = GetComponent<NavMeshAgent>();

        _player = PlayerBehavior.PlayerBehaviorInstance.transform;
    }

    private void Update()
    {
        _sliderEnemyHealth.ChangeColorSlidere(EnemyHP);

        if (_navMeshAgent.remainingDistance < 0.2f && !_navMeshAgent.pathPending)
        {
            StartCoroutine(MoveToNextPatrolPoint());
        }

        Vector3 point = new Vector3(transform.position.x, transform.position.y * 25f, transform.position.z);
        _ray.origin = point;
        _ray.direction = transform.forward;
        Debug.DrawRay(point, transform.forward * 5, Color.yellow);

        Pla();
    }

    private void Pla()
    {
        if (Physics.Raycast(_ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.GetComponent<PlayerBehavior>())
            {
                _enemyAnimator.SetTrigger("PistolWalk");
                _enemyShoot.Shoot();
            }
        }
    }

    public int EnemyHP
    {
        get => _enemyHP;
        set
        {
            _enemyHP = value;
            if (_enemyHP <= 0)
            {
                DeathEnemy();
            }
        }
    }

    public void DeathEnemy()
    {
        _navMeshAgent.isStopped = true;

        _imgChange.Change();

        _enemyAnimator.SetTrigger("Dead");

        CreateKey();
        GlobalEventManager.SendOpeningDoor();
        _sliderEnemyHealth.SliderEnemyHealth.gameObject.SetActive(false);

        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<EnemyBehavior>().enabled = false;
        GlobalEventManager.SendEnemyKilled();
        _uIBehavior._killed++;
    }

    private void InitializePatrolRoute()
    {
        foreach (Transform child in _patrolRoute)
        {
            _locations.Add(child);
        }
    }

    private IEnumerator MoveToNextPatrolPoint()
    {
        if (_locations.Count == 0) yield return null;

        _navMeshAgent.destination = _locations[_indexLocation].position;

        _indexLocation = (_indexLocation + 1) % _locations.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBehavior>())
        {
            _navMeshAgent.destination = _player.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HitBoody();
        }
    }
    public void HitHeadshot()
    {
        EnemyHP -= _maxHP;
    }

    public void HitBoody()
    {
        EnemyHP -= _damage;
        _enemyAnimator.SetTrigger("Hit");
    }

    private void CreateKey()
    {
        Instantiate(_key, _keyTransform.position, _keyTransform.rotation);
    }
}
