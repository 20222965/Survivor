using UnityEngine;

public class EnemyReposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;
        if(GameManager.Instance == null) return;

        if (coll.enabled)
        {
            var playerPos = GameManager.Instance.Player.transform.position;
            Vector3 distance = playerPos - transform.position;
            Vector3 rand = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
            transform.Translate(distance * 2 + rand);
        }
    }
}
