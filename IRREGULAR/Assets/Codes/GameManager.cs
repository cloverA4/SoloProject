using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
