using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    /// <summary>
    /// 무기 레벨 및 스탯 정보
    /// </summary>
    public WeaponStatus Status { get; set; }
    protected Player player;

    /// <summary>
    /// 무기 기본값 설정
    /// </summary>
    /// <param name="data">해당 무기의 데이터</param>
    /// <param name="_player">무기를 사용하는 플레이어</param>
    /// <param name="_equipment">플레이어의 장비</param>
    public virtual void Init(WeaponData data, Player _player, PlayerEquipment _equipment)
    {
        Status = new WeaponStatus(data, _equipment);

        player = _player;
        transform.parent = _player.transform;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        WeaponInit();
    }
    public virtual void Init(WeaponData data)
    {
        Init(data, GameManager.Instance.Player, GameManager.Instance.Player.GetComponent<PlayerEquipment>());
    }

    /// <summary>
    /// 같은 아이템인지 확인
    /// </summary>
    /// <param name="item">확인할 아이템</param>
    /// <returns>true: 같은 아이템, false: 다른 아이템</returns>
    public bool CompareItem(ItemData item) => Status.CompareItem(item);


    /// <summary>
    /// 무기 레벨업
    /// </summary>
    public virtual void LevelUp()
    {
        Status.LevelUp();
        LevelUpSetting();
    }

    /// <summary>
    /// 무기 기본 설정
    /// </summary>
    protected abstract void WeaponInit();

    /// <summary>
    /// 레벨업 했을 때 업데이트 할 내용
    /// </summary>
    protected virtual void LevelUpSetting() { }

    /// <summary>
    /// 무기 공격 실행
    /// </summary>
    public abstract void Attack();
}