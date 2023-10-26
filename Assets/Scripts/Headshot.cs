using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : MonoBehaviour
{
    [SerializeField] private EnemyBehavior EnemyBehavior;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            EnemyBehavior.HitHeadshot();
        }
    }
}
