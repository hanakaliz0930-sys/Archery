using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource StaffSFXSource;
    public AudioSource birdSFXSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Awake()
    {
        instance = this;
    }

    // Update is called once per frame

    public void PlayStaffSFX(AudioClip clip)
    {
        StaffSFXSource.PlayOneShot(clip);
        StaffSFXSource.volume = 0.5f;
    }
    public void PlayBirdSFX(AudioClip clip)
    {
        birdSFXSource.PlayOneShot(clip);        
        birdSFXSource.volume = 5f;
    }
}
