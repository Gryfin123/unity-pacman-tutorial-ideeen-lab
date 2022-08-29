using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _pelletAudioSource;
    [SerializeField] private float _defaultVolume = 1f;

    [Header("Pellet Sound Settings")]
    // Current duration for pellet sound loop
    [SerializeField] private float _chompSoundDuration = 0f;
    // How long after picking pellet, pellet eating sound is supposed to keep on playing
    [SerializeField] private float _chompSoundIncrement = 0f;
    // Max for how long after picking pellet, pellet eating sound is supposed to keep on playing
    [SerializeField] private float _chompSoundMaxReserve = 0.4f;  

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
        if (_chompSoundDuration > 0)
        {
            _pelletAudioSource.volume = _defaultVolume;
            _chompSoundDuration -= Time.deltaTime;
        }
        else
        {
            _pelletAudioSource.volume = 0;
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
        _chompSoundDuration += _chompSoundIncrement;
        if (_chompSoundDuration > _chompSoundMaxReserve) _chompSoundDuration = _chompSoundMaxReserve;
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
