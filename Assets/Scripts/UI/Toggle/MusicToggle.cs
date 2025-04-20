using UnityEngine;

public class MusicToggle : BaseVolumeToggle
{
    private void Start()
    {
        toggle.isOn = PlayerPrefs.GetInt(StaticStringUI.AudioString.SFXString.TOGGLE_SFX, 1) == 1;
    }

    public override void OnValueChanged(bool value)
    {
        if (IsAudioMixerLoaded())
        {
            float dB = value ? Mathf.Lerp(StaticConst.MIN_DB, StaticConst.MAX_DB, PlayerPrefs.GetFloat(StaticStringUI.AudioString.MusicString.MUSIC_VOLUME, 1)) : StaticConst.MIN_DB;
            audioMixer.SetFloat(StaticStringUI.AudioString.MusicString.MUSIC_VOLUME, dB);

            // Save music volume
            PlayerPrefs.SetInt(StaticStringUI.AudioString.SFXString.TOGGLE_SFX, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
