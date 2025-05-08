using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private AudioSource successAudio;

    [SerializeField] private ParticleSystem failureParticles;
    [SerializeField] private AudioSource failureAudio;

    [SerializeField] private AudioSource OVFX;
    [SerializeField] private AudioSource XVFX;

    public void PlayProblemVFX(bool result)
    {
        if (result && OVFX != null)
        {
            OVFX.Play();
        }
        else if (XVFX  != null)
        {
            XVFX.Play();
        }
    }

    public void PlayResultEffect(bool result)
    {
        if (result)
        {
            if (!successParticles.isPlaying)
            {
                successParticles.Play();
            }
            if (successAudio != null)
            {
                successAudio.Play();
            }
        }
        else
        {
            // if (!failureParticles.isPlaying)
            // {
            //     failureParticles.Play();
            // }
            if (failureAudio != null) 
            {
                failureAudio.Play();
            }
        }
    }
}
