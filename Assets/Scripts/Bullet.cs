using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Zombie>(out Zombie zombie))
        {
            zombie.getBulletDamage(transform);
        } else if(collision.gameObject.TryGetComponent<BarrelFire>(out BarrelFire barrelfire))
        {
            barrelfire.Destroy();
        }

        Destroy(gameObject);
    }
}
