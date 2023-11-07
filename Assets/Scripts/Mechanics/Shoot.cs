using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    
    public float xVelocity;
    public float yVelocity;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (xVelocity <= 0) xVelocity = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log("Please set default values on " + gameObject.name);
    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.initVel = new Vector2(xVelocity, yVelocity);
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.initVel = new Vector2(-xVelocity, yVelocity);
        }
    }
}
