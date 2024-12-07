using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TreeBase : MonoBehaviour
{
    public float SoundProximityRadius = 20f;
    private AudioSource _audioSource;
    private Transform _player;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindWithTag("Player").transform;

        if (_audioSource != null)
        {
            _audioSource.loop = true;
            _audioSource.Stop();
        }
    }

    protected void Update() {
        if (_player == null || _audioSource == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= SoundProximityRadius)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }

    public abstract void UpdateBehavior();
}
