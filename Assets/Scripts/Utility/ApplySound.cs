using UnityEngine;

public class ApplySound : MonoBehaviour
{
    AudioSource audiosource;
    public bool randomizePitch = false;
    public float pitchRange = 0.2f;

    public void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void PlayRandomSound(AudioClip[] clips)
    {
        int choice = Random.Range(0, clips.Length);

        if (randomizePitch)
            audiosource.pitch = Random.Range(1.0f - pitchRange, 1.0f + pitchRange);

        audiosource.PlayOneShot(clips[choice]);
    }

    public void PlaySoundOneTime(AudioClip clip)
    {
        audiosource.PlayOneShot(clip);
    }

}
