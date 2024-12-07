using System.Collections;
using UnityEngine;

public class EnemyTree : TreeBase
{
    /// <summary>
    /// Movement & position related
    /// </summary>
    public float MoveSpeed = 2f;
    public float DirectionChangeInterval = 3f; // time before changing direction
    public float MaxWanderRadius = 10f; // limit how far tree can wander from spawn point
    private Vector3 _currentDirection;

    private Vector3 _spawnPosition;

    /// <summary>
    /// Firing related
    /// </summary>
    public GameObject SeedPrefab;
    public float FireInterval;
    public float SeedVelocity;
    private float nextFire;
    // transform of the player object, used to make seeds travel towards player
    private Transform player;

    private void Start()
    {
        SetUpAudio();
        _spawnPosition = transform.position; // initial position
        player = FindObjectOfType<Player>().transform;
        nextFire = Time.time + FireInterval;
        StartCoroutine(ChangeDirectionRoutine());
    }

    private void Update()
    {
        TreeUpdate();
        UpdateBehavior();

        // fire if it's time
        if (Time.time > nextFire)
        {
            Fire();
            nextFire += FireInterval;
        }
    }

    /// <summary>
    /// Fire a seed in the direction of the player
    /// </summary>
    private void Fire()
    {
        var playerDirection = (player.position - transform.position);
        playerDirection.y = 0;
        playerDirection.Normalize();
        var verticalOffset = new Vector3(0, 1.5f, 0);
        var seed = Instantiate(SeedPrefab, transform.position + verticalOffset, Quaternion.identity);
        seed.GetComponent<Rigidbody>().velocity = SeedVelocity * playerDirection;
    }

    public override void UpdateBehavior()
    {
        // move in current direction
        transform.position += _currentDirection * MoveSpeed * Time.deltaTime;

        // limit wandering distance
        if (Vector3.Distance(transform.position, _spawnPosition) > MaxWanderRadius)
            // move back toward spawn position if out of bounds
            _currentDirection = (_spawnPosition - transform.position).normalized;
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(DirectionChangeInterval);

            // choose new random direction
            _currentDirection = new Vector3(
                Random.Range(-1f, 1f),
                0,
                Random.Range(-1f, 1f)
            ).normalized;
        }
    }
}