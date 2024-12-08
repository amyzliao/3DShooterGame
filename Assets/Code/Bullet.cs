using Code;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private AudioSource _audioSource;

    /// <summary>
    ///     the plane that everything is on
    /// </summary>
    private Bounds planeBounds;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        var plane = GameObject.Find("Plane");
        planeBounds = plane.GetComponent<Collider>().bounds;
    }

    /// <summary>
    ///     Destroy ourselves if we've left the plane bounds
    /// </summary>
    private void Update()
    {
        if (false
            || transform.position.x < planeBounds.min.x
            || transform.position.x > planeBounds.max.x
            || transform.position.z < planeBounds.min.z
            || transform.position.z > planeBounds.max.z)
            Destroy(gameObject);
    }

    /// <summary>
    ///     Handle bullet collissions
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        // collide with enemies, add to score and disappear
        if (other.gameObject.TryGetComponent<EnemyTree>(out _))
        {
            ScoreManager.AddToScore(1);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // collide with good trees, sub from score and disappear
        else if (other.gameObject.TryGetComponent<TreeBase>(out _))
        {
            ScoreManager.AddToScore(-1);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}