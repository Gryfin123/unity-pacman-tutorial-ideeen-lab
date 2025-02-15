using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _pelletAudioSource;
    [SerializeField] private AudioSource _backgroundAudioSource;
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
    [SerializeField] private AudioClip _backgroundStandard;
    [SerializeField] private AudioClip _backgroundFrightened;
    [SerializeField] private AudioClip _pelletChomp;
    [SerializeField] private AudioClip _eatFruit;
    [SerializeField] private AudioClip _eatGhost;
    [SerializeField] private AudioClip _death;
    [SerializeField] private AudioClip _click;

    private void Start() {
        if (_pelletAudioSource != null) _pelletAudioSource.clip = _pelletChomp;
        if (_backgroundAudioSource != null) _backgroundAudioSource.clip = _backgroundStandard;
    }

    private void Update() {
        if (_pelletAudioSource != null) ProcessChompingSound();
    }

    /// Prcess how to play pellet eating sounds
    private void ProcessChompingSound()
    {
        // Process Pellet sounds
        if (_chompSoundDuration > 0)
        {
            _pelletAudioSource.volume = _defaultVolume * 0.2f;
            _chompSoundDuration -= Time.deltaTime;
        }
        else
        {
            _pelletAudioSource.volume = 0;
        }
    }

    public void PlayBackgroundMusicStandard()
    {
        if (_backgroundAudioSource != null)
        {
            _backgroundAudioSource.volume = _defaultVolume * 0.85f;
            _backgroundAudioSource.clip = _backgroundStandard;
            _backgroundAudioSource.Play();
        }
    }

    public void PlayBackgroundMusicFrightened()
    {
        if (_backgroundAudioSource != null)
        {
            _backgroundAudioSource.volume = _defaultVolume * 0.85f;
            _backgroundAudioSource.clip = _backgroundFrightened;
            _backgroundAudioSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (_backgroundAudioSource != null)
        {
            _backgroundAudioSource.Stop();
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
    public void PlayClick()
    {
        PlayClip(_click);
    }
}
