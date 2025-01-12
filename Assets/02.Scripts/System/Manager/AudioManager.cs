using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum EAudioMixerType { Master, BGM, SFX }

    public static AudioManager Instance { get; private set; }

    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private bool[] isMute = new bool[3];
    [SerializeField] private float[] audioVolumes = { 0.7f, 0.7f, 0.7f };

    [Header("BGM")]
    public AudioClip[] bgmClips;
    AudioSource bgmPlayer;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public int channelCount;
    AudioSource[] sfxPlayers;
    int channelIdx;

    public enum BGM { Lobby, Game }
    public enum Sfx { None, Dead, Hit, LevelUp, Lose, Melee, Range, Select, Win, Cancel, Coin, Throw, Notice }

    private void Awake()
    {
        Instance = this;

        Init();
    }

    /// <summary>
    /// ����� �÷��̾� �� �ͼ� ����
    /// </summary>
    private void Init()
    {

        // BGM PLAYER INIT
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];  // BGM �׷쿡 ����
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;

        // SFX PLAYER INIT
        GameObject sfxObject = new GameObject("SfxObject");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channelCount];
        for (int i = 0; i < channelCount; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0]; // SFX �׷쿡 ����
            sfxPlayers[i].playOnAwake = false;
        }

        // Awake���� SetFloat�� �۵����� �ʴ� ���װ� ����.
        Invoke(nameof(Load), 0);
    }


    /// <summary>
    /// ����� BGM
    /// </summary>
    /// <param name="bgm">BGM ����</param>
    /// <param name="isPlay">true => bgm ���, false => ������� ���� �ߴ�</param>
    public void PlayBGM(BGM bgm, bool isPlay = true)
    {
        if (isPlay)
        {
            bgmPlayer.clip = bgmClips[(int)bgm];
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.clip = bgmClips[(int)bgm];
            bgmPlayer.Stop();
        }
    }

    /// <summary>
    /// ȿ���� ���
    /// </summary>
    /// <param name="sfx">����� ȿ����</param>
    public void PlaySfx(Sfx sfx)
    {
        if (sfx == Sfx.None) return;

        for (int i = 0; i < channelCount; i++)
        {
            int loopIdx = (i + channelIdx) % channelCount;

            if (!sfxPlayers[loopIdx].isPlaying)
            {
                channelIdx = loopIdx;
                sfxPlayers[loopIdx].clip = sfxClips[(int)sfx - 1];
                sfxPlayers[loopIdx].Play();
                break;
            }
        }
    }

    /// <summary>
    /// ���� ������ ��ȯ
    /// </summary>
    /// <param name="audioMixerType">���� ũ�⸦ �� �Ҹ� ����</param>
    /// <returns></returns>
    public float GetVolume(EAudioMixerType audioMixerType)
    {
        return audioVolumes[(int)audioMixerType];
    }

    /// <summary>
    /// ���� ���Ұ� ���� ��ȯ
    /// </summary>
    /// <param name="audioMixerType">���Ұ� ���¸� �� �Ҹ� ����</param>
    /// <returns></returns>
    public bool IsMute(EAudioMixerType audioMixerType)
    {
        return isMute[(int)audioMixerType];
    }

    /// <summary>
    /// ���� ũ�� ��ȯ
    /// </summary>
    /// <param name="audioMixerType">������ �ٲ� �Ҹ� ����</param>
    /// <param name="volume">������(0.0001~1)</param>
    public void SetAudioVolume(EAudioMixerType audioMixerType, float volume)
    {
        if (!isMute[(int)audioMixerType])   // ���ҰŰ� �ƴ� ��쿡�� �� ����
        {   // ����� �ͼ��� ���� -80 ~ 0. volume �� 0.0001 ~ 1�� �޾� Log10 * 20�� �ؼ� -80 ~ 0���� ��ȯ
            audioMixer.SetFloat(audioMixerType.ToString(), Mathf.Log10(volume) * 20);
        }
        // ���� ���� ���� ũ�� ����
        audioVolumes[(int)audioMixerType] = volume;
    }

    /// <summary>
    /// ���Ұ� ���� ��ȯ
    /// </summary>
    /// <param name="audioMixerType">���Ұ��� �Ҹ� ����</param>
    /// <param name="mute">���Ұ� ��ȯ. true => ���Ұ�</param>
    public void SetAudioMute(EAudioMixerType audioMixerType, bool mute)
    {
        int type = (int)audioMixerType;
        if (mute) // ���Ұ�
        {
            isMute[type] = true;    // Mute 0.001f
            SetMuteVolume(audioMixerType);
        }
        else
        {
            isMute[type] = false;
            SetAudioVolume(audioMixerType, audioVolumes[type]); // �����ߴ� ������ �Ҹ� �缳��
        }
    }

    /// <summary>
    /// ���Ұ� �������� ���� (Ŭ���� ���ο��� ���)
    /// </summary>
    /// <param name="audioMixerType">���Ұ��� �Ҹ� ����</param>
    private void SetMuteVolume(EAudioMixerType audioMixerType)
    {
        audioMixer.SetFloat(audioMixerType.ToString(), -80f);
    }

    /// <summary>
    /// ���� ���� �� ��Ʈ ���� ����
    /// </summary>
    public void Save()
    {
        foreach (EAudioMixerType audioMixerType in Enum.GetValues(typeof(EAudioMixerType)))
        {
            int type = (int)audioMixerType;
            PlayerPrefs.SetInt(audioMixerType.ToString()+"Mute",isMute[type] ? 1 : 0);
            PlayerPrefs.SetFloat(audioMixerType.ToString(), audioVolumes[type]);
        }
    }

    /// <summary>
    /// ����� ���� ���� �� ��Ʈ ���� �ε�
    /// </summary>
    public void Load()
    {
        foreach (EAudioMixerType audioMixerType in Enum.GetValues(typeof(EAudioMixerType)))
        {
            int type = (int)audioMixerType;
            if (PlayerPrefs.HasKey(audioMixerType.ToString()))
            {   // ����� �����Ͱ� ���� ��� �ε�
                isMute[type] = PlayerPrefs.GetInt(audioMixerType.ToString() + "Mute") == 1 ? true : false;
                audioVolumes[type] = PlayerPrefs.GetFloat(audioMixerType.ToString());
            }
            else
            {   // ����� �����Ͱ� ���� �� �⺻��
                audioVolumes[type] = 0.7f;
                isMute[type] = false;
            }

            // �ε�� ������ ���� ����
            SetAudioVolume(audioMixerType, audioVolumes[type]);
            SetAudioMute(audioMixerType, isMute[type]);
        }
    }
}
