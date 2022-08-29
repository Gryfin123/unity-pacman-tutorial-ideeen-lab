using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _pelletAudioSource;
    [SerializeField] private bool _hasChompSoundQueued = false;
    [SerializeField] private float _defaultVolume = 1f;

    [Header("Clip References")]
    [SerializeField] private AudioClip _introSong;
    [SerializeField] private AudioClip _pelletChomp;
    [SerializeField] private AudioClip _eatFruit;
    [SerializeField] private AudioClip _eatGhost;
    [SerializeField] private AudioClip _death;

    private void Update() {
        ProcessChompingSound();
    }

    /// Prcess how to play pellet eating sounds
    private void ProcessChompingSound()
    {
        // Process Pellet sounds
        if (!_pelletAudioSource.isPlaying && _hasChompSoundQueued)
        {
            _pelletAudioSource.clip = _pelletChomp;
            _pelletAudioSource.volume = _defaultVolume;
            _pelletAudioSource.Play();
            _hasChompSoundQueued = false;
        }
    }


    /// Play sounds on demand
    private void PlayClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, _defaultVolume);
    }

    public void PlayIntro()
    {
        PlayClip(_introSong);
    }
    public void PlayChomp()
    {
        if (!_pelletAudioSource.isPlaying)
        {
        _hasChompSoundQueued = true;
        }
    }
    public void PlayEatFruit()
    {
        PlayClip(_eatFruit);
    }
    public void PlayEatGhost()
    {
        PlayClip(_eatGhost);
    }
    public void PlayDeath()
    {
        PlayClip(_death);
    }
}
