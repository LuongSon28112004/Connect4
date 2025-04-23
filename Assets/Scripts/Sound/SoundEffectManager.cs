using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundEffectMananger : SoundManager
{
    public static SoundEffectMananger Instance { get; private set; }

    private new void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        base.Awake();
    }

    protected override void LoadAudioClips()
    {
        clips = new();

        AsyncOperationHandle<IList<AudioClip>> handle = Addressables.LoadAssetsAsync<AudioClip>(
            new List<string>() { "SoundEffect" },
            addressables =>
            {
                if (addressables != null)
                {
                    clips.Add(addressables);
                }
            },
            Addressables.MergeMode.Union,
            false
        );

        handle.Completed += Load_Completed;
    }

    private void Load_Completed(AsyncOperationHandle<IList<AudioClip>> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogWarning("Sound Effect could not loaded!");
            return;
        }
    }

    protected override void InitialVolume()
    {
        float value = PlayerPrefs.GetFloat(StaticStringUI.AudioString.SFXString.SFX_VOLUME, 1);
        float dB = Mathf.Lerp(StaticConst.MIN_DB, StaticConst.MAX_DB, value);
        if (PlayerPrefs.GetInt(StaticStringUI.AudioString.SFXString.TOGGLE_SFX, 1) == 0)
        {
            dB = StaticConst.MIN_DB;
        }
        audioMixer.SetFloat(StaticStringUI.AudioString.SFXString.SFX_VOLUME, dB);
    }
}
