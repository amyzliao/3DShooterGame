using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree : TreeBase
{
    public float MoveSpeed = 2f;
    public float DirectionChangeInterval = 3f; // time before changing direction
    public float MaxWanderRadius = 10f; // limit how far tree can wander from spawn point

    private Vector3 _spawnPosition;
    private Vector3 _currentDirection;

    private void Start()
    {
        _spawnPosition = transform.position; // initial position
        StartCoroutine(ChangeDirectionRoutine());
    }

    public override void UpdateBehavior()
    {
        // move in current direction
        transform.position += _currentDirection * MoveSpeed * Time.deltaTime;

        // limit wandering distance
        if (Vector3.Distance(transform.position, _spawnPosition) > MaxWanderRadius)
        {
            // move back toward spawn position if out of bounds
            _currentDirection = (_spawnPosition - transform.position).normalized;
        }
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