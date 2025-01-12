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
    /// 오디오 플레이어 및 믹서 설정
    /// </summary>
    private void Init()
    {

        // BGM PLAYER INIT
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];  // BGM 그룹에 연결
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;

        // SFX PLAYER INIT
        GameObject sfxObject = new GameObject("SfxObject");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channelCount];
        for (int i = 0; i < channelCount; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0]; // SFX 그룹에 연결
            sfxPlayers[i].playOnAwake = false;
        }

        // Awake에서 SetFloat이 작동하지 않는 버그가 있음.
        Invoke(nameof(Load), 0);
    }


    /// <summary>
    /// 재생할 BGM
    /// </summary>
    /// <param name="bgm">BGM 종류</param>
    /// <param name="isPlay">true => bgm 재생, false => 재생중인 음악 중단</param>
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
    /// 효과음 재생
    /// </summary>
    /// <param name="sfx">재생할 효과음</param>
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
    /// 현재 볼륨을 반환
    /// </summary>
    /// <param name="audioMixerType">볼륨 크기를 볼 소리 종류</param>
    /// <returns></returns>
    public float GetVolume(EAudioMixerType audioMixerType)
    {
        return audioVolumes[(int)audioMixerType];
    }

    /// <summary>
    /// 현재 음소거 상태 반환
    /// </summary>
    /// <param name="audioMixerType">음소거 상태를 볼 소리 종류</param>
    /// <returns></returns>
    public bool IsMute(EAudioMixerType audioMixerType)
    {
        return isMute[(int)audioMixerType];
    }

    /// <summary>
    /// 볼륨 크기 변환
    /// </summary>
    /// <param name="audioMixerType">볼륨을 바꿀 소리 종류</param>
    /// <param name="volume">볼륨값(0.0001~1)</param>
    public void SetAudioVolume(EAudioMixerType audioMixerType, float volume)
    {
        if (!isMute[(int)audioMixerType])   // 음소거가 아닐 경우에만 값 변경
        {   // 오디오 믹서의 값은 -80 ~ 0. volume 값 0.0001 ~ 1을 받아 Log10 * 20을 해서 -80 ~ 0으로 변환
            audioMixer.SetFloat(audioMixerType.ToString(), Mathf.Log10(volume) * 20);
        }
        // 새로 들어온 볼륨 크기 저장
        audioVolumes[(int)audioMixerType] = volume;
    }

    /// <summary>
    /// 음소거 상태 변환
    /// </summary>
    /// <param name="audioMixerType">음소거할 소리 종류</param>
    /// <param name="mute">음소거 변환. true => 음소거</param>
    public void SetAudioMute(EAudioMixerType audioMixerType, bool mute)
    {
        int type = (int)audioMixerType;
        if (mute) // 음소거
        {
            isMute[type] = true;    // Mute 0.001f
            SetMuteVolume(audioMixerType);
        }
        else
        {
            isMute[type] = false;
            SetAudioVolume(audioMixerType, audioVolumes[type]); // 저장했던 값으로 소리 재설정
        }
    }

    /// <summary>
    /// 음소거 볼륨으로 조정 (클래서 내부에서 사용)
    /// </summary>
    /// <param name="audioMixerType">음소거할 소리 종류</param>
    private void SetMuteVolume(EAudioMixerType audioMixerType)
    {
        audioMixer.SetFloat(audioMixerType.ToString(), -80f);
    }

    /// <summary>
    /// 음악 볼륨 및 뮤트 상태 저장
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
    /// 저장된 음악 볼륨 및 뮤트 상태 로드
    /// </summary>
    public void Load()
    {
        foreach (EAudioMixerType audioMixerType in Enum.GetValues(typeof(EAudioMixerType)))
        {
            int type = (int)audioMixerType;
            if (PlayerPrefs.HasKey(audioMixerType.ToString()))
            {   // 저장된 데이터가 있을 경우 로드
                isMute[type] = PlayerPrefs.GetInt(audioMixerType.ToString() + "Mute") == 1 ? true : false;
                audioVolumes[type] = PlayerPrefs.GetFloat(audioMixerType.ToString());
            }
            else
            {   // 저장된 데이터가 없을 시 기본값
                audioVolumes[type] = 0.7f;
                isMute[type] = false;
            }

            // 로드된 값으로 볼륨 설정
            SetAudioVolume(audioMixerType, audioVolumes[type]);
            SetAudioMute(audioMixerType, isMute[type]);
        }
    }
}
