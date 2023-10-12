using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Slider Exp;
    [SerializeField] Text LevelText;
    [SerializeField] Text TimeText;
    [SerializeField] Text MonsterKillText;
    [SerializeField] Slider HPSlider;

   

    private void LateUpdate()
    {
        //경험치
        float curExp = GameManager.instance.exp;
        float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.playerlevel, GameManager.instance.nextExp.Length - 1)];
        Exp.value = curExp / maxExp;

        //레벨텍스트
        LevelText.text = string.Format("Lv.{0:F0}", GameManager.instance.playerlevel);
        //인덱스순번 : F0은 소수점자릿수
        MonsterKillText.text = string.Format("{0:F0}", GameManager.instance.kill);
        //시간텍스트
        float remainTime = GameManager.instance.maxGameTime + GameManager.instance.gameTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);
        TimeText.text = string.Format("{0:D2}:{1:D2}", min, sec);
        //플레이어 체력바
        float curHealth = GameManager.instance.playerHealth;
        float maxHealth = GameManager.instance.playerMaxHealth;
        HPSlider.value = curHealth / maxHealth;
    }
}
