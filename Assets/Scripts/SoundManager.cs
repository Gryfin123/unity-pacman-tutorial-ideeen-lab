using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private float _defaultVolume = 1f;

    [Header("Clip References")]
    [SerializeField] private AudioClip _introSong;
    [SerializeField] private AudioClip _eatPellet;
    [SerializeField] private AudioClip _eatFruit;
    [SerializeField] private AudioClip _eatGhost;
    [SerializeField] private AudioClip _death;

    private void PlayClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, _defaultVolume);
    }

    public void PlayIntro()
    {
        PlayClip(_introSong);
    }
    public void PlayEatPellet()
    {
        PlayClip(_eatPellet);
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
