using UnityEngine;

public class RepositionMap : MonoBehaviour
{
    Collider2D _coll;

    private void Awake()
    {
        _coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 MyPos = transform.position;

        Vector3 PlayerPos = GameManager.Instance.Player.transform.position;

        
        // ���� ������ PlayerDir�� 0���� ������ -1 �Դϴ� �ƴ϶�� 1�� �ֽ��ϴ�      

        switch (transform.tag)
        {
            case "Ground" :
                float DiffX = PlayerPos.x - MyPos.x; // Mathf.Abs ���밪�� ���� �Լ�
                float DiffY = PlayerPos.y - MyPos.y;
                float DirX = DiffX < 0 ? -1 : 1;
                float DirY = DiffY < 0 ? -1 : 1;
                DiffX = Mathf.Abs(DiffX);
                DiffY = Mathf.Abs(DiffY);

                if (DiffX > DiffY){
                    transform.Translate(Vector3.right * DirX * 40); //�����̵�
                }
                else if (DiffX < DiffY){
                    transform.Translate(Vector3.up * DirY * 40); //�����̵�
                }
                break;
            case "Monster":
                if (_coll.enabled) { // �ݶ��̴��� ����ִٸ� �̰� ���߿� �׾����� ���� �׾����� ��ȣ�ۿ� �ȵǰ� ���鿹��
                    Vector3 dist = PlayerPos - MyPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2); 
                    // ����ũ�Ⱑ 20 �̱⿡ �÷��̾� �������� ī�޶� �Ⱥ��̴� �������� ���ġ�ϱ����� 5�� ����
                }
                break;
        }
    }
}
