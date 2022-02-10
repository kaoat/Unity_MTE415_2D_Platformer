using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip DeadSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip getDamageSound;
    [SerializeField] private AudioClip getBananaSound;
    [SerializeField] private AudioClip endGameSound;
    [SerializeField] private AudioClip getAppleSound;

    private AudioSource audioPlayer;

    public static AudioPlayer instance;

    public void SetVolume(float newVolume)
    {
        if (newVolume > 1)
        {
            audioPlayer.volume = 1;
        }else if (newVolume < 0)
        {
            audioPlayer.volume = 0;
        }
        else
        {
            audioPlayer.volume = newVolume;
        }
    }
    public float GetVolume()
    {
        return audioPlayer.volume;
    }

    private void Awake()
    {
        instance = this;
        audioPlayer = GetComponent<AudioSource>();
    }
    
    public void PlayJumpSound()
    {
        audioPlayer.PlayOneShot(jumpSound);
    }
    public void PlayDeadSound()
    {
        audioPlayer.PlayOneShot(DeadSound);
    }

    public void PlayAttackSound()
    {
        audioPlayer.PlayOneShot(attackSound);
    }

    public void PlayGetDamageSound()
    {
        audioPlayer.PlayOneShot(getDamageSound);
    }
    public void PlayEndGameSound()
    {
        audioPlayer.PlayOneShot(endGameSound);
    }

    public void PlayGetBananaSound()
    {
        audioPlayer.PlayOneShot(getBananaSound);
    }
    public void PlayGetAppleSound()
    {
        audioPlayer.PlayOneShot(getAppleSound);
    }
}
