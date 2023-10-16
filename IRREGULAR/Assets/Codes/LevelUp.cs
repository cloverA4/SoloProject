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
    }

    public void Hide()
    {
        _rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();
    }

    public void Select(int index)
    {
        _items[index].OnClick();
    }

    void Next()
    {
        // ��� ������ ��Ȱ��ȭ
        foreach (Item item in _items) { 
            item.gameObject.SetActive(false);
        }
        // ������ 3���� Ȱ��ȭ
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, _items.Length);
            ran[1] = Random.Range(0, _items.Length);
            ran[2] = Random.Range(0, _items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        if (GameManager.Instance.PlayerId == 0)
        {
            for (int index = 0; index < ran.Length; index++)
            {
                Item ranItem = _items[ran[index]];

                // ������ �Ǿ����� �Һ���������� ����
                if (ranItem.level == ranItem.Data.Damages.Length)
                {
                    _items[4].gameObject.SetActive(true); // �ϵ��ڵ� �������� ���ֱ� �̰Ÿ��� ���� ����
                }
                else
                    ranItem.gameObject.SetActive(true);
            }
        }

        if (GameManager.Instance.PlayerId == 1)
        {
            for (int index = 2; index < ran.Length; index++)
            {
                Item ranItem = _items[ran[index]];

                // ������ �Ǿ����� �Һ���������� ����
                if (ranItem.level == ranItem.Data.Damages.Length)
                {
                    _items[4].gameObject.SetActive(true); // �ϵ��ڵ� �������� ���ֱ� �̰Ÿ��� ���� ����
                }
                else
                    ranItem.gameObject.SetActive(true);
            }
        }

    }
}
