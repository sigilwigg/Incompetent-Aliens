using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource soundFXObject;

    public Music[] musicTracks;
    private string currentMusicId;

    private AudioSource musicSource;
    public string defaultMusicId;

    public float musicFadeDuration;

    void Awake()
    {
        if (instance == null) instance = this;

        SetupMusicTracks();
        ChangeMusic(defaultMusicId);


        GameObject[] existingAudioManagers = GameObject.FindGameObjectsWithTag("AudioManager");

        if (existingAudioManagers.Length > 1) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    void SetupMusicTracks()
    {
        foreach (Music track in musicTracks)
        {
            track.source = gameObject.AddComponent<AudioSource>();
            track.source.clip = track.clip;
            track.source.loop = true;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // ----- spawn object -----
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // ----- assign audio -----
        audioSource.clip = audioClip;

        // ----- assign volume -----
        audioSource.volume = volume;

        // ----- play sound -----
        audioSource.Play();

        // ----- get audio clip length -----
        float clipLength = audioSource.clip.length;

        // ----- destroy clip when done -----
        Destroy(audioSource.gameObject, clipLength);
    }

    public void ChangeMusic(string newMusicId)
    {
        // ------ stop current music -----
        foreach (Music track in musicTracks)
        {
            if (track.musicID == currentMusicId)
            {
                StartCoroutine(FadeOut(track.source));
                break;
            }
        }

        // ----- set new music id -----
        currentMusicId = newMusicId;

        // ----- play new music -----
        foreach (Music track in musicTracks)
        {
            if (track.musicID == currentMusicId)
            {
                StartCoroutine(FadeIn(track.source));
                break;
            }
        }
    }

    IEnumerator FadeIn(AudioSource source, float startVolume = 0, float endVolume = 1)
    {
        float timeElapsed = 0.0f;
        source.volume = startVolume;
        source.Play();

        while (timeElapsed < musicFadeDuration)
        {
            source.volume = Mathf.Lerp(startVolume, endVolume, timeElapsed / musicFadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        source.volume = endVolume;
    }

    IEnumerator FadeOut(AudioSource source, float startVolume = 1, float endVolume = 0)
    {
        float timeElapsed = 0.0f;
        source.volume = startVolume;

        while (timeElapsed < musicFadeDuration)
        {
            source.volume = Mathf.Lerp(startVolume, endVolume, timeElapsed / musicFadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        source.volume = endVolume;
        source.Stop();
    }
}
