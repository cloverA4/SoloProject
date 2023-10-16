using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    [SerializeField] AudioClip _bgmClip;
    [SerializeField] float _bgmVolume;
    [SerializeField] AudioSource _bgmPlayer;
    AudioHighPassFilter _bgmEffect;

    [Header("SFX")]
    [SerializeField] AudioClip[] _sfxClips;
    [SerializeField] float _sfxVolume;
    [SerializeField] int _channels;
    [SerializeField] AudioSource[] _sfxPlayers;
    [SerializeField] int _channelIndex;

    public enum Sfx 
    {
        Dead,
        Hit,
        LevelUp =3,
        Lose,
        Melee,
        Range =7,
        Select,
        Win,
    }

    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        _bgmPlayer = bgmObject.AddComponent<AudioSource>();
        _bgmPlayer.playOnAwake = false;
        _bgmPlayer.loop = true;
        _bgmPlayer.volume = _bgmVolume;
        _bgmPlayer.clip = _bgmClip;
        _bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        //효과음 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayers = new AudioSource[_channels];

        for (int index = 0; index < _sfxPlayers.Length; index++)
        {
            _sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayers[index].playOnAwake = false;
            _sfxPlayers[index].bypassListenerEffects = true;
            _sfxPlayers[index].volume = _sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay){
            _bgmPlayer.Play();
        }
        else { _bgmPlayer.Stop();}
    }

    public void EffectBgm(bool isPlay)
    {
        _bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < _sfxPlayers.Length; index++)
        {
            int loopIndex = (index + _channelIndex) % _sfxPlayers.Length;

            if (_sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ranIndex = Random.Range(0, 2);
            }

            _channelIndex = loopIndex;
            _sfxPlayers[loopIndex].clip = _sfxClips[(int)sfx];
            _sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
