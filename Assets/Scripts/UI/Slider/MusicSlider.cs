using UnityEngine;

public class MusicSlider : BaseVolumeSlider
{
    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(
            StaticStringUI.AudioString.MusicString.MUSIC_VOLUME,
            1.0f
        );

        // Nếu audioMixer đã được load thì khởi tạo volume ngay lập tức
        if (IsAudioMixerLoaded())
        {
            InitialVolume();
        }
    }

    public override void OnValueChanged(float value)
    {
        if (IsAudioMixerLoaded())
        {
            float dB = Mathf.Lerp(StaticConst.MIN_DB, StaticConst.MAX_DB, value); //ham noi suy tuyen tinh
            if (PlayerPrefs.GetInt(StaticStringUI.AudioString.SFXString.TOGGLE_SFX, 1) == 0)
            {
                dB = StaticConst.MIN_DB;
            }
            audioMixer.SetFloat(StaticStringUI.AudioString.MusicString.MUSIC_VOLUME, dB);

            // Save music volume
            PlayerPrefs.SetFloat(StaticStringUI.AudioString.MusicString.MUSIC_VOLUME, value);
        }
    }

    protected override void InitialVolume()
    {
        if (IsAudioMixerLoaded())
        {
            float dB = Mathf.Lerp(StaticConst.MIN_DB, StaticConst.MAX_DB, slider.value);
            if (PlayerPrefs.GetInt(StaticStringUI.AudioString.SFXString.TOGGLE_SFX, 1) == 0)
            {
                dB = StaticConst.MIN_DB;
            }
            audioMixer.SetFloat(StaticStringUI.AudioString.MusicString.MUSIC_VOLUME, dB);
        }
    }
}
