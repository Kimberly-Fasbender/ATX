using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{

    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip dieSFX;
    [SerializeField] AudioClip musicNoteSFX;
    [SerializeField] AudioClip speedSFX;
    [SerializeField] AudioClip thwompSFX;

    AudioListener audioListener;
    AudioClip currentSFX;

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
