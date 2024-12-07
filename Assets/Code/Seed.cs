using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;

public class Seed : MonoBehaviour
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
  void OnCollisionEnter(Collision other)
  {
    // collide with player, subtract 1 life
    if (other.gameObject.TryGetComponent<Player>(out _))
    {
      LivesManager.AddToLives(-1);
      Destroy(gameObject);
    }
    // disappear
    else
    {
      Destroy(gameObject);
    }
  }
}


