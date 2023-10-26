using UnityEngine;

public class BulletBehavoir : MonoBehaviour
{
    [SerializeField] private Transform _bulletMark;
    private float _destroyTimer = 4f;
    private SimpleShoot _simpleShoot;

    private void Start()
    {
        _simpleShoot = SimpleShoot.SimpleShootInstance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletMask"))
        {
            Vector3 position = collision.contacts[0].point;
            Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);

            var bulletMark = Instantiate(_bulletMark, position, rotation, collision.transform);

            Destroy(bulletMark.gameObject, _destroyTimer);
            Destroy(_simpleShoot._newBullet);
        }
    }
}
