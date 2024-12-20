using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    ///     Fields for firing bullets
    /// </summary>
    public GameObject BulletPrefab;

    public float FireInterval;
    public float BulletVelocity = 100f;
    private FiringSound _firingSound;

    private Rigidbody _playerRb;
    private AudioSource _walkingAudioSource;

    /// <summary>
    ///     The camera attached to the player
    /// </summary>
    private Transform CameraTransform;

    private float nextFire;

    // Start is called before the first frame update
    private void Start()
    {
        nextFire = 0f;
        CameraTransform = transform.GetChild(0).transform;
        _playerRb = GetComponent<Rigidbody>();
        _walkingAudioSource = GetComponent<AudioSource>();
        _firingSound = GetComponentInChildren<FiringSound>();
    }

    private void Update()
    {
        if (transform.position.y < -1.0f)
            LevelManager.GameOver();

        MaybeFire();
        PlayWalkingSound();
    }

    private void PlayWalkingSound()
    {
        if (_playerRb.velocity != Vector3.zero && !_walkingAudioSource.isPlaying)
            _walkingAudioSource.Play();
        else if (_playerRb.velocity == Vector3.zero && _walkingAudioSource.isPlaying)
            _walkingAudioSource.Stop();
    }

    /// <summary>
    ///     Fire if player is pushing button
    ///     and cooldown is over
    /// </summary>
    private void MaybeFire()
    {
        var fire = Input.GetButtonDown("Fire1");
        if (fire && Time.time > nextFire)
        {
            FireBullet();
            nextFire = Time.time + FireInterval;
        }
    }

    private void FireBullet()
    {
        _firingSound.PlaySound();
        var verticalOffset = new Vector3(0, 0.5f, 0);
        var bullet = Instantiate(BulletPrefab, transform.position + verticalOffset, Quaternion.identity);
        bullet.transform.rotation = CameraTransform.rotation;
        bullet.GetComponent<Rigidbody>().velocity = BulletVelocity * bullet.transform.forward;
    }
}