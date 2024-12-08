using UnityEngine;

public class FiringSound : MonoBehaviour
{
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}