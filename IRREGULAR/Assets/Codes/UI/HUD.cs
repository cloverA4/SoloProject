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
        float curExp = GameManager.Instance.exp;
        float maxExp = GameManager.Instance.nextExp[Mathf.Min(GameManager.Instance.playerlevel, GameManager.Instance.nextExp.Length - 1)];
        Exp.value = curExp / maxExp;

        //�����ؽ�Ʈ
        LevelText.text = string.Format("Lv.{0:F0}", GameManager.Instance.playerlevel);
        //�ε������� : F0�� �Ҽ����ڸ���
        MonsterKillText.text = string.Format("{0:F0}", GameManager.Instance.kill);
        //�ð��ؽ�Ʈ
        float remainTime = GameManager.Instance.maxGameTime + GameManager.Instance.gameTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);
        TimeText.text = string.Format("{0:D2}:{1:D2}", min, sec);
        //�÷��̾� ü�¹�
        float curHealth = GameManager.Instance.playerHealth;
        float maxHealth = GameManager.Instance.playerMaxHealth;
        HPSlider.value = curHealth / maxHealth;
    }
}
