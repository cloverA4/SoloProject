using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    // 프리팹들을 보관할 변수
    [SerializeField] GameObject[] _prefabs;

    // 풀담당을 하는 리스트들
    List<GameObject>[] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[_prefabs.Length]; //프리팹의 길이만큼

        for (int index = 0; index < _pools.Length; index++) // for문 시작문,조건문,중감문
        {
            _pools[index] = new List<GameObject>();
        }

        Debug.Log(_pools.Length);
    }

    public GameObject Get(int index) //게임오브젝트를 반환하는 함수 //몬스터 생성
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 (비활성화 된)있는 게임오브젝트 접근
        foreach (GameObject item in _pools[index]) // 배열, 리스트들의 데이터를 순차적으로 접근하는 반복문
        {
            if (!item.activeSelf) // 비활성화된 오브젝트
            {
                // ... 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... 못 찾았으면?
        if (select == null) // 미리 선언한 변수가 계속 비어있으면
        {
            // ... 새롭게 생성하고 select 변수에 할당
            select = Instantiate(_prefabs[index], transform); //transform위치에 오브젝트 풀링 생성 (poolmanager)
            _pools[index].Add(select);
        }
        return select;
    }
}
