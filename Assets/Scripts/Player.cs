using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player Stats")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float health = 100;

    [Header("Sound Effects")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float projectionFireFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start() {
        SetUpMoveBondaries();
    }

    // Update is called once per frame
    void Update() {
        Move();
        Fire();
    }

    private void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(FireContunuously());
        }

        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContunuously() {
        while (true) {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectionFireFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer) {
        health = Mathf.Max(health - damageDealer.GetDamage(), 0);
        damageDealer.Hit();
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
    }

    private void Move() {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newPositionX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newPositionY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newPositionX, newPositionY);
    }

    private void SetUpMoveBondaries() {
        var gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public float GetHealth() {
        return health;
    }
}
