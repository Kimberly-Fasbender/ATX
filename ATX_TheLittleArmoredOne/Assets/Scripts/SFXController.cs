using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{

    [SerializeField] public AudioClip jumpSFX;
    [SerializeField] public AudioClip dieSFX;
    [SerializeField] public AudioClip musicNoteSFX;
    [SerializeField] public AudioClip speedSFX;
    [SerializeField] public AudioClip thwompSFX;

    AudioListener audioListener;
    AudioClip currentSFX;
    // AudioSource audioSource;

    void Start()
    {
        audioListener = GetComponent<AudioListener>();
    }

    public void PlaySFX(string clipName, float volume)
    {   
        AssignAudioClip(clipName);
        AudioSource.PlayClipAtPoint(currentSFX, audioListener.transform.position, volume);
    }

    private void AssignAudioClip(string clipName)
    {
        switch(clipName)
        {
            case "jump":
                currentSFX = jumpSFX;
                break;
            case "die":
                currentSFX = dieSFX;
                break;
            case "speed":
                currentSFX = speedSFX;
                break;
            case "music note":
                currentSFX = musicNoteSFX;
                break;
            case "thwomp":
                currentSFX = thwompSFX;
                break;
        }
    }
}
