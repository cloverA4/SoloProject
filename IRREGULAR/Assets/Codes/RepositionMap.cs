using UnityEngine;

public class RepositionMap : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 MyPos = transform.position;

        Vector3 _PlayerPos = PlayerController.Instance.transform.position;

        float DiffX = Mathf.Abs(_PlayerPos.x - MyPos.x); // Mathf.Abs 절대값에 대한 함수
        float DiffY = Mathf.Abs(_PlayerPos.y - MyPos.y);

        Vector3 PlayerDir = PlayerController.Instance.InputVec;
        float DirX = PlayerDir.x < 0 ? -1 : 1;
        float DirY = PlayerDir.y < 0 ? -1 : 1;
        // 삼항 연산자 PlayerDir가 0보다 작으면 -1 입니다 아니라면 1을 넣습니다      

        switch (transform.tag)
        {
            case "Ground" :
                if (DiffX > DiffY){
                    transform.Translate(Vector3.right * DirX * 40); //수평이동
                }
                else if (DiffX < DiffY){
                    transform.Translate(Vector3.up * DirY * 40); //수직이동
                }
                break;
            case "Enemy":

                break;
        }
    }
}
