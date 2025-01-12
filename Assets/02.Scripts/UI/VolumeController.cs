using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class VolumeController : MonoBehaviour
{
    public Slider[] slider = new Slider[3];
    public Button[] muteBtn = new Button[3];
    public TextMeshProUGUI[] text = new TextMeshProUGUI[3];

    public Sprite VolumeMusicImage;
    public Sprite MuteMusicImage;

    private bool[] isMute = new bool[3];

    private bool[] saveMuteState = new bool[3];
    private float[] volume = new float[3];

    public void OnEnable()
    {
        foreach (AudioManager.EAudioMixerType audioMixerType in Enum.GetValues(typeof(AudioManager.EAudioMixerType)))
        {
            int type = (int)audioMixerType;
            volume[type] = AudioManager.Instance.GetVolume(audioMixerType);
            isMute[type] = saveMuteState[type] = AudioManager.Instance.IsMute(audioMixerType);

            slider[type].SetValueWithoutNotify(volume[type]);
            muteBtn[type].image.sprite = GetVolumeImage(isMute[type]);
            SetText(audioMixerType, volume[type]);
        }
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
    }

    // ���Ұ� ON OFF
    private void ToggleMuteImage(AudioManager.EAudioMixerType audioMixerType)
    {
        int idx = (int)audioMixerType;
        isMute[idx] = !isMute[idx];

        muteBtn[idx].image.sprite = GetVolumeImage(isMute[idx]);
    }

    private Sprite GetVolumeImage(bool isMute)
    {
        if (isMute)
        {
            return MuteMusicImage;
        }
        else
        {
            return VolumeMusicImage;
        }
    }

    // Mute On/Off ��ư ������ �۵�
    public void OnMuteMaster()
    {
        ToggleMuteImage(AudioManager.EAudioMixerType.Master);
        AudioManager.Instance.SetAudioMute(AudioManager.EAudioMixerType.Master, isMute[(int)AudioManager.EAudioMixerType.Master]);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
    }
    public void OnMuteBGM()
    {
        ToggleMuteImage(AudioManager.EAudioMixerType.BGM);
        AudioManager.Instance.SetAudioMute(AudioManager.EAudioMixerType.BGM, isMute[(int)AudioManager.EAudioMixerType.BGM]);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
    }
    public void OnMuteSFX()
    {
        ToggleMuteImage(AudioManager.EAudioMixerType.SFX);
        AudioManager.Instance.SetAudioMute(AudioManager.EAudioMixerType.SFX, isMute[(int)AudioManager.EAudioMixerType.SFX]);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
    }

    // ���� �����̴� ���� ����Ǹ� ȣ��
    public void OnVolumeMasterChanged()
    {
        int type = (int)AudioManager.EAudioMixerType.Master;
        SetText(AudioManager.EAudioMixerType.Master, slider[type].value);
        AudioManager.Instance.SetAudioVolume(AudioManager.EAudioMixerType.Master, slider[type].value);
    }
    public void OnVolumeBGMChanged()
    {
        int type = (int)AudioManager.EAudioMixerType.BGM;
        SetText(AudioManager.EAudioMixerType.BGM, slider[type].value);
        AudioManager.Instance.SetAudioVolume(AudioManager.EAudioMixerType.BGM, slider[type].value);
    }
    public void OnVolumeSFXChanged()
    {
        int type = (int)AudioManager.EAudioMixerType.SFX;
        SetText(AudioManager.EAudioMixerType.SFX, slider[type].value);
        AudioManager.Instance.SetAudioVolume(AudioManager.EAudioMixerType.SFX, slider[type].value);
    }

    public void SetText(AudioManager.EAudioMixerType audioMixerType, float volume)
    {
        int type = (int)audioMixerType;
        switch (audioMixerType)
        {
            case AudioManager.EAudioMixerType.Master:
                text[type].text = $"��ü ���� <color=orange> {(int)(slider[type].value * 100)}</color>";
                break;
            case AudioManager.EAudioMixerType.BGM:
                text[type].text = $"����� <color=orange> {(int)(slider[type].value * 100)}</color>";
                break;
            case AudioManager.EAudioMixerType.SFX:
                text[type].text = $"ȿ���� <color=orange> {(int)(slider[type].value * 100)}</color>";
                break;
            default:
                break;
        }
    }

    public void Save()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.Instance.Save();
        return;
    }

    public void Cancel()
    {
        foreach(AudioManager.EAudioMixerType type in Enum.GetValues(typeof(AudioManager.EAudioMixerType)))
        {
            AudioManager.Instance.SetAudioMute(type, saveMuteState[(int)type]);
            AudioManager.Instance.SetAudioVolume(type, volume[(int)type]);
        }

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Cancel);
    }
}
