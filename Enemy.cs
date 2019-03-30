using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Status")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 250;

    [Header("Projectile")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] AudioClip projectileSFX;
    [SerializeField] [Range(0, 1)] float projectileSFXVolume = .5f;
    [SerializeField] float projectileYSpawnOffset = .5f;

    [Header("Damage")]
    [SerializeField] AudioClip hitTakenSFX;
    [SerializeField] [Range(0, 1)] float hitTakeSFXVolume = .5f;

    [Header("Death")]
    [SerializeField] GameObject enemyDeathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = .5f;

    string playerTag;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountdownToShot();
    }


    //Firing
    private void CountdownToShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        Vector3 laserPos = new Vector3(transform.position.x, transform.position.y - projectileYSpawnOffset);
        GameObject laser = Instantiate(projectile,
                               laserPos,
                               Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(projectileSFX, laserPos, projectileSFXVolume);
    }


    //Collision/Health
    private void ProcessDeath()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue, playerTag);
        Vector3 deathPos = new Vector3(transform.position.x, transform.position.y);
        GameObject deathVFX = Instantiate(enemyDeathVFX, deathPos, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        Destroy(gameObject);
        Destroy(deathVFX, durationOfExplosion);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(other, damageDealer);
    }

    private void ProcessHit(Collider2D other, DamageDealer damageDealer)
    {
        playerTag = other.tag;
        health -= damageDealer.GetDamage();
        if (health > 0)
        {
            AudioSource.PlayClipAtPoint(hitTakenSFX, Camera.main.transform.position, hitTakeSFXVolume);
        }
        damageDealer.Hit();
        if (health <= 0)
        {
            ProcessDeath();
        }
    }
}
