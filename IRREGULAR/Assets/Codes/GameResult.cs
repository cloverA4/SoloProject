using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    [SerializeField] GameObject[] _titles;

    public void Win()
    {
        _titles[0].SetActive(true);
    }
    public void Lose()
    {
        _titles[1].SetActive(true);
    }

    
}
