using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManger : MonoBehaviour
{
    [SerializeField] GameObject[] _lockCharacacter;
    [SerializeField] GameObject[] _unlockCharacacter;
    [SerializeField] GameObject _uiNotice;
    Achive[] _achives;

    WaitForSecondsRealtime _wait;

    enum Achive 
    {
        UnlockSaint,
    }


    private void Awake()
    {
        _achives = (Achive[])Enum.GetValues(typeof(Achive));
        _wait = new WaitForSecondsRealtime(5);
        if (!PlayerPrefs.HasKey("MyData"))
            Init();
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in _achives){
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    private void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for (int index = 0; index < _lockCharacacter.Length; index++)
        {
            string achiveName = _achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            _lockCharacacter[index].SetActive(!isUnlock);
            _unlockCharacacter[index].SetActive(isUnlock);
        }
    }

    private void LateUpdate()
    {
        foreach (Achive achive in _achives) {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockSaint:
                isAchive = GameManager.Instance.gameTime == GameManager.Instance.maxGameTime;
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0){
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for (int index = 0; index < _uiNotice.transform.childCount; index++){
                bool isActive = index == (int)achive;
                _uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        _uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return _wait;

        _uiNotice.SetActive(false);
    }
}
