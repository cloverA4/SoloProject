using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    // �����յ��� ������ ����
    [SerializeField] GameObject[] _prefabs;

    // Ǯ����� �ϴ� ����Ʈ��
    List<GameObject>[] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[_prefabs.Length]; //�������� ���̸�ŭ

        for (int index = 0; index < _pools.Length; index++) // for�� ���۹�,���ǹ�,�߰���
        {
            _pools[index] = new List<GameObject>();
        }

        Debug.Log(_pools.Length);
    }

    public GameObject Get(int index) //���ӿ�����Ʈ�� ��ȯ�ϴ� �Լ� //���� ����
    {
        GameObject select = null;

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��)�ִ� ���ӿ�����Ʈ ����
        foreach (GameObject item in _pools[index]) // �迭, ����Ʈ���� �����͸� ���������� �����ϴ� �ݺ���
        {
            if (!item.activeSelf) // ��Ȱ��ȭ�� ������Ʈ
            {
                // ... �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... �� ã������?
        if (select == null) // �̸� ������ ������ ��� ���������
        {
            // ... ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(_prefabs[index], transform); //transform��ġ�� ������Ʈ Ǯ�� ���� (poolmanager)
            _pools[index].Add(select);
        }
        return select;
    }
}
