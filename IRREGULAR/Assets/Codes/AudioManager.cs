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

    [Header("SFX")]
    [SerializeField] AudioClip[] _sfxClips;
    [SerializeField] float _sfxVolume;
    [SerializeField] int _channels;
    [SerializeField] AudioSource[] _sfxPlayer;
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

        //효과음 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayer = new AudioSource[_channels];

        for (int index = 0; index < _sfxPlayer.Length; index++)
        {
            _sfxPlayer[index] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayer[index].playOnAwake = false;
            _sfxPlayer[index].volume = _sfxVolume;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        _sfxPlayer[0].clip = _sfxClips[(int)sfx];
        _sfxPlayer[0].Play();
    }
}
