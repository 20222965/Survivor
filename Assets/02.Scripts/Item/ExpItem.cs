using System.Collections;
using UnityEngine;

public class ExpItem : PoolAble
{

    public int Exp { get; set; }

    int[] expSpriteLevel = { 1, 5, 10 };
    public Sprite[] expSprites;

    bool isAttracting = false;
    bool isActive = false;

    Player player;
    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        player = GameManager.Instance.Player;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += player.MagnetStrength * Time.deltaTime * direction;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive || GameManager.Instance.IsPause) return;

        if (isAttracting)   // �ڼ� ȿ�� �ߵ����̶��
        {
            if (collision.transform.CompareTag("Player"))
            {   // �÷��̾�� �������
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Coin);   // ����ġ ȹ�� ȿ����
                isAttracting = false;   // �ڼ� ȿ�� ���� (ȹ��)
                player.AddExp(Exp); // �÷��̾� ����ġ ����
                isActive = false;
                ReleaseObject();  // ����ġ ������Ʈ ��ȯ
            }
            return;
        }


        // �÷��̾� �ڼ��� ����
        if (collision.CompareTag("Magnet"))
        {   // �ڼ� ȿ�� On
            isAttracting = true;
            StartCoroutine(AttractingPlayer()); // �÷��̾ ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isActive || GameManager.Instance.IsPause) return;

        // ���� ������ ���, ����
        if (collision.CompareTag("Area"))
        {   // ����ġ ����
            ExpManager.Instance.SaveExpOutsideArea(Exp);
            isActive = false;
            ReleaseObject();
            return;
        }
    }
}
