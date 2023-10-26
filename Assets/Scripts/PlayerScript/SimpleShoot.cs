using UnityEngine;
[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    public static SimpleShoot SimpleShootInstance;

    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    [HideInInspector] public GameObject _newBullet;
    [Header("Location Refrences")]
    [SerializeField] private Animator Animator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;
    [SerializeField] private TestCamBehavior _testCam;
    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 100f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;
    [HideInInspector] public int _amountBullet = 10;
    private int _addBullets = 10;
    private float _currentTime;
    private float _outTime = 0.3f;

    private void Awake()
    {
        SimpleShootInstance = this;
    }

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (Animator == null)
            Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            if (!bulletPrefab)
            { return; }

            if (_currentTime > _outTime && _amountBullet >= 1)
            {
                _currentTime = 0;
                Animator.SetTrigger("Fire");
                _newBullet = Instantiate(bulletPrefab, _testCam.transform.position, _testCam.transform.rotation);
                _amountBullet--;
                _newBullet.GetComponent<Rigidbody>().AddForce(_testCam.transform.forward * shotPower);
                if (muzzleFlashPrefab)
                {
                    GameObject tempFlash;
                    tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
                    Destroy(tempFlash, destroyTimer);
                }
            }
        }
    }

    public void AddBullet()
    {
        _amountBullet += _addBullets;
    }
    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }
}
