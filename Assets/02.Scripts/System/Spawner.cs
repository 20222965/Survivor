using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] SpawnData[] datas;

    /// <summary>
    /// ���� ��Ÿ�� ��� (���� ���� ������ ��Ÿ�� �̻��� ��� ��Ÿ�� ��)
    /// </summary>
    float[] spawnTimers;

    /// <summary>
    /// ���� Ƚ�� ��� (���� ���� ���� �̻��� ��� �������� ����)
    /// </summary>
    int[] spawnCounter;

    /// <summary>
    /// ���� ��ġ
    /// </summary>
    Transform[] spawnPoint;

    bool isReady;
    void Awake()
    {
        spawnTimers = new float[datas.Length];
        spawnCounter = new int[datas.Length];
        for (int i = 0; i < spawnTimers.Length; ++i)
        {   // ù ������ ���� ��Ÿ�� ����
            spawnTimers[i] = float.MaxValue;
        }
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Init()
    {
        for (int i = 0; i < datas.Length; ++i)
        {   // �� ������ ������Ʈ Ǯ�� �߰�
            PoolManager.Instance.Add(datas[i].EnemyData, 100);
        }
    }

    void Update()
    {
        if (!isReady)
        {
            Init();
            isReady = true;
        }

        if (GameManager.Instance.IsPause)
            return;

        Spawn();
    }

    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    void Spawn()
    {
        for (int i = 0; i < datas.Length; ++i)
        {
            var data = datas[i];

            // ���� �ð��� �ƴ� ��� �������� ����. ���� �ð��� 0������ ��� ��� ����.
            if (data.SpawnStartTime > GameManager.Instance.GameTime || (data.SpawnEndTime > 0 && data.SpawnEndTime < GameManager.Instance.GameTime))
                continue;

            // ���� Ƚ�� �ʰ��� ��� �������� ����. ���� �������Ѱ��� 0 ���� ��� ���� ����.
            if (data.SpawnMaxCount > 0 && data.SpawnMaxCount <= spawnCounter[i])
                continue;

            // ���� ��Ÿ���� ��� �������� ����
            if (data.SpawnCoolTime > spawnTimers[i])
            {
                spawnTimers[i] += Time.deltaTime;
                continue;
            }

            // ���� ��Ÿ���� ������ ��� ����
            spawnTimers[i] = 0f;
            ++spawnCounter[i];
            var enemy = (Enemy)PoolManager.Instance.Get(data.EnemyData.DataID);
            enemy.Init(data);
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        }
    }
}