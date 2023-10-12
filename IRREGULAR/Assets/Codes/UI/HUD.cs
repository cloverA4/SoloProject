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
        //����ġ
        float curExp = GameManager.instance.exp;
        float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.playerlevel, GameManager.instance.nextExp.Length - 1)];
        Exp.value = curExp / maxExp;

        //�����ؽ�Ʈ
        LevelText.text = string.Format("Lv.{0:F0}", GameManager.instance.playerlevel);
        //�ε������� : F0�� �Ҽ����ڸ���
        MonsterKillText.text = string.Format("{0:F0}", GameManager.instance.kill);
        //�ð��ؽ�Ʈ
        float remainTime = GameManager.instance.maxGameTime + GameManager.instance.gameTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);
        TimeText.text = string.Format("{0:D2}:{1:D2}", min, sec);
        //�÷��̾� ü�¹�
        float curHealth = GameManager.instance.playerHealth;
        float maxHealth = GameManager.instance.playerMaxHealth;
        HPSlider.value = curHealth / maxHealth;
    }
}
