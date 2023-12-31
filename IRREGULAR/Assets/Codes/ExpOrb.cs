using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (GameManager.Instance.PoolManager.Prefabs[3])
        {
            GameManager.Instance.GetExp(1);
        }
        else if (GameManager.Instance.PoolManager.Prefabs[4])
        {
            GameManager.Instance.GetExp(2);
        }
        else if (GameManager.Instance.PoolManager.Prefabs[5])
        {
            GameManager.Instance.GetExp(3);
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ExpOrb);
        gameObject.SetActive(false);
    }
}
