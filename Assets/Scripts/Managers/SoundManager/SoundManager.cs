using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;    // Start is called before the first frame update
    [SerializeField] private float startPitch = 0.7f;
    [SerializeField] private float addPitch = 0.2f;

    private AudioSource _audioSource;

    public void Construct(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }
    private void Start()
    {
        RefreshPitch();
    }
    public void PlayNoteSound(int index)
    {
        _audioSource.PlayOneShot(sounds[index], 1f);
        _audioSource.pitch += addPitch;
        _audioSource.pitch = Mathf.Clamp(_audioSource.pitch, 0.4f, 1.5f);
    }
    public void RefreshPitch()
    {
        _audioSource.pitch = startPitch;
    }
}
