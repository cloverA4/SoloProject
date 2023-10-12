using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    RectTransform _rect;
    void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        _rect.position = Camera.main.WorldToScreenPoint(GameManager.Instance.Player.transform.position + new Vector3(0, -1.2f, 0)); 
        // 월드 상의 오브젝트 위치를 스크린 좌표로 변환하는 것
        // vector를 맞춰서 플레이어와의 위치 조정
    }
}
