using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  /// <summary>
  /// Fields for firing bullets
  /// </summary>
  public GameObject BulletPrefab;
  public float FireInterval;
  public float BulletVelocity = 100f;
  private float nextFire;

  // Start is called before the first frame update
  void Start()
  {
    nextFire = 0f;
  }

  void Update()
  {
    MaybeFire();
  }

  /// <summary>
  /// Fire if player is pushing button
  /// and cooldown is over
  /// </summary>
  void MaybeFire()
  {
    var fire = Input.GetButtonDown("Fire1");
    if (fire && Time.time > nextFire)
    {
      FireBullet();
      nextFire = Time.time + FireInterval;
    }
  }

  void FireBullet()
  {
    var verticalOffset = new Vector3(0, 0.5f, 0);
    var bullet = Instantiate(BulletPrefab, transform.position + verticalOffset + transform.forward, Quaternion.identity);
    var orientation = gameObject.transform.GetChild(0).transform.rotation;
    bullet.transform.rotation = orientation;
    bullet.GetComponent<Rigidbody>().velocity = BulletVelocity * bullet.transform.forward;
  }

}


