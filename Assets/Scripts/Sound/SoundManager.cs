using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class SoundManager : MonoBehaviour
{
    [SerializeField]
    protected List<AudioClip> clips;

    [SerializeField]
    protected AudioSource audioSource;

    [SerializeField]
    protected AudioMixer audioMixer;

    protected void Awake()
    {
        LoadComponents();
        LoadAudioClips();
        LoadAudioMixer();
    }

    private void LoadComponents()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void LoadAudioMixer()
    {
        // Load one audio mixer
        AsyncOperationHandle<AudioMixer> handle = Addressables.LoadAssetAsync<AudioMixer>(
            "MixerController"
        );

        handle.Completed += Load_Completed;
    }

    private void Load_Completed(AsyncOperationHandle<AudioMixer> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogWarning("Audio mixer could not loaded!");
            return;
        }

        audioMixer = handle.Result;
        InitialVolume();
    }

    public void PlaySound(string name)
    {
        AudioClip clip = clips.Find(clip => clip.name == name);

        if (clip == null)
        {
            Debug.LogWarning($"Sound {name} not found!");
            return;
        }

        if (audioSource.isPlaying && audioSource.clip == clip)
            return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    protected abstract void LoadAudioClips();

    protected abstract void InitialVolume();
}
