using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config parameters
    [SerializeField] AudioClip startSFX;
    [SerializeField] float startSFXVolume = 1f;

    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 300;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.3f;
    [SerializeField] AudioClip projectileSFX;
    [SerializeField] [Range(0, 1)] float projectileSFXVolume = .5f;

    [Header("Damage")]
    [SerializeField] AudioClip hitTakenSFX;
    [SerializeField] [Range(0, 1)] float hitTakeSFXVolume = .5f;

    [Header("Death")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 2.5f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = .5f;


    AudioSource myAudioSource;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    bool firing;

    int player1HP;
    int player2HP;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        //AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position, startSFXVolume);
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    //Firing

    private void Fire()
    {
        if (gameObject.tag == "Player1")
        {
            if (Input.GetButtonDown("Fire1") && !firing)
            {
                firingCoroutine = StartCoroutine(FireContinuously());
                firing = true;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(firingCoroutine);
                firing = false;
            }
        }
        else if (gameObject.tag == "Player2")
        {
            if (Input.GetButtonDown("Fire1.5") && !firing)
            {
                firingCoroutine = StartCoroutine(FireContinuously());
                firing = true;
            }
            if (Input.GetButtonUp("Fire1.5"))
            {
                StopCoroutine(firingCoroutine);
                firing = false;
            }
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            Vector3 laserPos = new Vector3(transform.position.x, transform.position.y + .5f);
            GameObject laser = Instantiate(laserPrefab,
                laserPos, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(projectileSFX, laserPos, projectileSFXVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    //Movement

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void Move()
    {
        if (gameObject.tag == "Player1")
        {
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

            var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            transform.position = new Vector2(newXPos, newYPos);
        }
        else if (gameObject.tag == "Player2")
        {
            var deltaY = Input.GetAxis("Vertical2") * Time.deltaTime * moveSpeed;
            var deltaX = Input.GetAxis("Horizontal2") * Time.deltaTime * moveSpeed;

            var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            transform.position = new Vector2(newXPos, newYPos);
        }
    }


    //Collision/Health
    public int GetHealth(string tag)
    {
        if (health > 0)
        {
            return health;
        }
        else
        {
            health = 0;
            return health;
        }
    }

    private void ProcessDeath()
    {
        Vector3 deathPos = new Vector3(transform.position.x, transform.position.y);
        GameObject playerDeathVFX = Instantiate(deathVFX, deathPos, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        Destroy(gameObject);
        Destroy(playerDeathVFX, durationOfExplosion);
        if (FindObjectsOfType(GetType()).Length < 2)
        {
            FindObjectOfType<Level>().LoadGameOver();
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(other, damageDealer);
    }

    private void ProcessHit(Collider2D other, DamageDealer damageDealer)
    {
        AudioSource.PlayClipAtPoint(hitTakenSFX, Camera.main.transform.position, hitTakeSFXVolume);
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            ProcessDeath();
        }
    }
}
