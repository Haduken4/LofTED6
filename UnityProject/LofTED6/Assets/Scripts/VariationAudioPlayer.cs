using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariationAudioPlayer : MonoBehaviour
{
    public AudioSource SoundPlayer = null;
    public float PitchVariation = 0.05f;
    public float VolumeVariation = 0.02f;

    float ogPitch = 0;
    float ogVol = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(SoundPlayer != null)
        {
            ogPitch = SoundPlayer.pitch;
            ogVol = SoundPlayer.volume;
        }
    }

    public void PlaySound()
    {
        if(SoundPlayer != null && !SoundPlayer.isPlaying)
        {
            SoundPlayer.pitch = ogPitch + Random.Range(-PitchVariation, PitchVariation);
            SoundPlayer.volume = ogVol + Random.Range(-VolumeVariation, VolumeVariation);

            SoundPlayer.Play();
        }
    }
}
