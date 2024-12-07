using System.Collections;
using System.Collections.Generic;
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
  private void Update()
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
}

