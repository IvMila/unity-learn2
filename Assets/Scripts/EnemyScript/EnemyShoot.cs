using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public int _amountBullet = 10;
    private float _timeOut = 1f;
    private float _currentTime;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 0.5f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;
    [SerializeField] private EnemyBehavior enemyBehavior;
   
    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;
    }

    public void Shoot()
    {
        if (!bulletPrefab)
        { return; }

        if (_currentTime > _timeOut && _amountBullet >= 1)
        {
            _currentTime = 0;
            gunAnimator.SetTrigger("Fire");
            GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(enemyBehavior._ray.direction * shotPower);

            _amountBullet--;
            Destroy(bullet, destroyTimer);
            if (muzzleFlashPrefab)
            {
                GameObject tempFlash;
                tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
                Destroy(tempFlash, destroyTimer);
            }
        }
    }
    
    void CasingRelease()
    {
        if (!casingExitLocation || !casingPrefab)
        { return; }

        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;

        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);

        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        Destroy(tempCasing, destroyTimer);
    }
}
