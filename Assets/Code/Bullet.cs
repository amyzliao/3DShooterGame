using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;

public class Bullet : MonoBehaviour
{

  /// <summary>
  /// the plane that everything is on
  /// </summary>
  private Bounds planeBounds;

  void Start()
  {
    var plane = GameObject.Find("Plane");
    planeBounds = plane.GetComponent<Collider>().bounds;
  }

  /// <summary>
  /// Destroy ourselves if we've left the plane bounds
  /// </summary>
  void Update()
  {
    if (false
        || transform.position.x < planeBounds.min.x
        || transform.position.x > planeBounds.max.x
        || transform.position.z < planeBounds.min.z
        || transform.position.z > planeBounds.max.z)
    {
      Destroy(gameObject);
    }
  }

  /// <summary>
  /// Handle bullet collissions
  /// </summary>
  /// <param name="other"></param>
  void OnTriggerEnter(Collider other)
  {
    // collide with player, that's fine
    if (other.gameObject.TryGetComponent<Player>(out _))
    {
      return;
    }
    // collide with enemies, add to score and disappear
    else if (other.gameObject.TryGetComponent<EnemyTree>(out _))
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

