using System.Collections;
using UnityEngine;

public class ExpItem : PoolAble
{

    public int Exp { get; set; }

    [SerializeField] int[] expSpriteLevel = { 1, 5, 10 };
    [SerializeField] public Sprite[] expSprites;

    [SerializeField] Magnet target;
    [SerializeField] SpriteRenderer spriteRenderer;

    bool isAttracting = false;
    bool isActive = false;

    private void Awake()
    {
        if(target == null) target = GameManager.Instance.Player.GetComponentInChildren<Magnet>(true);
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 오브젝트 활성화 및 경험치를 저장하고 일정 경험치마다 스프라이트 변환
    /// </summary>
    /// <param name="exp">드롭할 경험치 저장</param>
    public void Init(int exp)
    {
        Exp = exp;
        for(int i = expSpriteLevel.Length - 1; i >= 0; i--)
        {
            if(exp >= expSpriteLevel[i])
            {
                spriteRenderer.sprite = expSprites[i];
                isActive = true;
                return;
            }
        }

        spriteRenderer.sprite = expSprites[0];
        isActive = true;
    }

    /// <summary>
    /// 플레이어를 따라가는 함수 (자석)
    /// </summary>
    /// <returns></returns>
    public IEnumerator AttractingPlayer()
    {
        while (isActive && isAttracting && transform != null)
        {
            if (GameManager.Instance.IsPause)
                yield return null;

            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += target.MagnetStrength * Time.deltaTime * direction;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive || GameManager.Instance.IsPause) return;

        if (isAttracting)   // 자석 효과 발동중이라면
        {
            if (collision.transform.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.Player)))
            {   // 플레이어와 닿았으면
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Coin);   // 경험치 획득 효과음
                isAttracting = false;   // 자석 효과 종료 (획득)
                GameManager.Instance.Player.AddExp(Exp); // 플레이어 경험치 증가
                isActive = false;
                ReleaseObject();  // 경험치 오브젝트 반환
            }
            return;
        }


        // 플레이어 자석에 닿음
        if (collision.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.Magnet)))
        {   // 자석 효과 On
            isAttracting = true;
            StartCoroutine(AttractingPlayer()); // 플레이어를 따라감
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isActive || GameManager.Instance.IsPause) return;

        // 범위 밖으로 벗어남, 디스폰
        if (collision.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.Area)))
        {   // 경험치 저장
            ExpManager.Instance.SaveExpOutsideArea(Exp);
            isActive = false;
            ReleaseObject();
            return;
        }
    }
}
