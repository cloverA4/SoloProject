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

        
        // 삼항 연산자 PlayerDir가 0보다 작으면 -1 입니다 아니라면 1을 넣습니다      

        switch (transform.tag)
        {
            case "Ground" :
                float DiffX = PlayerPos.x - MyPos.x; // Mathf.Abs 절대값에 대한 함수
                float DiffY = PlayerPos.y - MyPos.y;
                float DirX = DiffX < 0 ? -1 : 1;
                float DirY = DiffY < 0 ? -1 : 1;
                DiffX = Mathf.Abs(DiffX);
                DiffY = Mathf.Abs(DiffY);

                if (DiffX > DiffY){
                    transform.Translate(Vector3.right * DirX * 40); //수평이동
                }
                else if (DiffX < DiffY){
                    transform.Translate(Vector3.up * DirY * 40); //수직이동
                }
                break;
            case "Monster":
                if (_coll.enabled) { // 콜라이더가 살아있다면 이건 나중에 죽었을때 꺼서 죽었을때 상호작용 안되게 만들예정
                    Vector3 dist = PlayerPos - MyPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2); 
                    // 맵의크기가 20 이기에 플레이어 기준으로 카메라가 안보이는 구간에서 재배치하기위해 5를 더함
                }
                break;
        }
    }
}
