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
        // ���� ���� ������Ʈ ��ġ�� ��ũ�� ��ǥ�� ��ȯ�ϴ� ��
        // vector�� ���缭 �÷��̾���� ��ġ ����
    }
}
