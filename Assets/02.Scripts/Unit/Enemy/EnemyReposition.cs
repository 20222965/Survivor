using UnityEngine;

public class EnemyReposition : MonoBehaviour
{
    [SerializeField] Collider2D coll2D;

    void Awake()
    {
        if(coll2D == null) coll2D = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.Area))) return;
        if(GameManager.Instance == null) return;

        if (coll2D.enabled)
        {
            var playerPos = GameManager.Instance.Player.transform.position;
            Vector3 distance = playerPos - transform.position;
            Vector3 rand = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
            transform.Translate(distance * 2 + rand);
        }
    }
}
