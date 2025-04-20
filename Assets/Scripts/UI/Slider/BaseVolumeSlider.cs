using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class BaseVolumeSlider : BaseSlider
{
    [SerializeField]
    protected AudioMixer audioMixer;

    private bool isAudioMixerLoaded = false;

    protected override void Awake()
    {
        base.Awake();
        LoadAudioMixer();
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
            Debug.LogWarning("Some assets could not be loaded");
            return;
        }

        audioMixer = handle.Result;
        isAudioMixerLoaded = true;
        InitialVolume();  // Chỉ khởi tạo khi đã load xong
    }

    protected abstract void InitialVolume();

    protected bool IsAudioMixerLoaded()
    {
        return isAudioMixerLoaded;
    }
}
