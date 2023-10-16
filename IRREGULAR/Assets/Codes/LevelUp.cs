using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform _rect;
    Item[] _items;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        _rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
        GameManager.Instance.OptionUiBtn.SetActive(false);
    }

    public void Hide()
    {
        _rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
        GameManager.Instance.OptionUiBtn.SetActive(true);
    }

    public void Select(int index)
    {
        _items[index].OnClick();
    }

    void Next()
    {
        // 모든 아이템 비활성화
        foreach (Item item in _items) { 
            item.gameObject.SetActive(false);
        }
        // 아이템 3개만 활성화
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, _items.Length);
            ran[1] = Random.Range(0, _items.Length);
            ran[2] = Random.Range(0, _items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }


        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = _items[ran[index]];

            // 만랩이 되었을때 소비아이템으로 변경
            if (ranItem.level == ranItem.Data.Damages.Length)
            {
                _items[4].gameObject.SetActive(true); // 하드코딩 마지막꺼 켜주기 이거말고 수정 요함
            }
            else
                ranItem.gameObject.SetActive(true);
        }
        

        

    }
}
