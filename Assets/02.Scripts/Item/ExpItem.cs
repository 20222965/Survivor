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
    /// ������Ʈ Ȱ��ȭ �� ����ġ�� �����ϰ� ���� ����ġ���� ��������Ʈ ��ȯ
    /// </summary>
    /// <param name="exp">����� ����ġ ����</param>
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
    /// �÷��̾ ���󰡴� �Լ� (�ڼ�)
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

        if (isAttracting)   // �ڼ� ȿ�� �ߵ����̶��
        {
            if (collision.transform.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.Player)))
            {   // �÷��̾�� �������
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Coin);   // ����ġ ȹ�� ȿ����
                isAttracting = false;   // �ڼ� ȿ�� ���� (ȹ��)
                GameManager.Instance.Player.AddExp(Exp); // �÷��̾� ����ġ ����
                isActive = false;
                ReleaseObject();  // ����ġ ������Ʈ ��ȯ
            }
            return;
        }


        // �÷��̾� �ڼ��� ����
        if (collision.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.Magnet)))
        {   // �ڼ� ȿ�� On
            isAttracting = true;
            StartCoroutine(AttractingPlayer()); // �÷��̾ ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isActive || GameManager.Instance.IsPause) return;

        // ���� ������ ���, ����
        if (collision.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.Area)))
        {   // ����ġ ����
            ExpManager.Instance.SaveExpOutsideArea(Exp);
            isActive = false;
            ReleaseObject();
            return;
        }
    }
}
