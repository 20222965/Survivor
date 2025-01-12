using UnityEngine;

public class Movement : MonoBehaviour
{

    Player player;
    PlayerEquipment playerEquip;
    Rigidbody2D rigid;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerEquip = GetComponent<PlayerEquipment>();
        rigid = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (GameManager.Instance.IsPause)
            return;

        Move();
    }

    void Move()
    {
        if (player.MoveDirection != Vector2.zero)
        {
            var NextVec = rigid.position + playerEquip.Status.MoveSpeed * Time.fixedDeltaTime * player.MoveDirection;
            rigid.MovePosition(NextVec);
        }
    }


}
