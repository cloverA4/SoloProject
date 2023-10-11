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
            case "Monster":
                if (_coll.enabled) { // 콜라이더가 살아있다면 이건 나중에 죽었을때 꺼서 죽었을때 상호작용 안되게 만들예정
                    transform.Translate(PlayerDir * 25 + new Vector3(Random.Range(-3f,3f), Random.Range(-3f, 3f),0f)); 
                    // 맵의크기가 20 이기에 플레이어 기준으로 카메라가 안보이는 구간에서 재배치하기위해 5를 더함
                }
                break;
        }
    }
}
