using UnityEngine;

public class RepositionMap : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 MyPos = transform.position;

        Vector3 _PlayerPos = PlayerController.Instance.transform.position;

        float DiffX = Mathf.Abs(_PlayerPos.x - MyPos.x); // Mathf.Abs ���밪�� ���� �Լ�
        float DiffY = Mathf.Abs(_PlayerPos.y - MyPos.y);

        Vector3 PlayerDir = PlayerController.Instance.InputVec;
        float DirX = PlayerDir.x < 0 ? -1 : 1;
        float DirY = PlayerDir.y < 0 ? -1 : 1;
        // ���� ������ PlayerDir�� 0���� ������ -1 �Դϴ� �ƴ϶�� 1�� �ֽ��ϴ�      

        switch (transform.tag)
        {
            case "Ground" :
                if (DiffX > DiffY){
                    transform.Translate(Vector3.right * DirX * 40); //�����̵�
                }
                else if (DiffX < DiffY){
                    transform.Translate(Vector3.up * DirY * 40); //�����̵�
                }
                break;
            case "Enemy":

                break;
        }
    }
}
