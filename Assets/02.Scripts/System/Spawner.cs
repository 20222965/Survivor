using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] SpawnData[] datas;

    /// <summary>
    /// 스폰 쿨타임 계산 (값이 스폰 데이터 쿨타임 이상일 경우 쿨타임 끝)
    /// </summary>
    float[] spawnTimers;

    /// <summary>
    /// 스폰 횟수 계산 (값이 스폰 제한 이상일 경우 스폰하지 않음)
    /// </summary>
    int[] spawnCounter;

    /// <summary>
    /// 스폰 위치
    /// </summary>
    Transform[] spawnPoint;

    bool isReady;
    void Awake()
    {
        spawnTimers = new float[datas.Length];
        spawnCounter = new int[datas.Length];
        for (int i = 0; i < spawnTimers.Length; ++i)
        {   // 첫 스폰은 스폰 쿨타임 없음
            spawnTimers[i] = float.MaxValue;
        }
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Init()
    {
        for (int i = 0; i < datas.Length; ++i)
        {   // 적 데이터 오브젝트 풀에 추가
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
    /// 적들을 스폰하는 함수
    /// </summary>
    void Spawn()
    {
        for (int i = 0; i < datas.Length; ++i)
        {
            var data = datas[i];

            // 스폰 시간이 아닐 경우 스폰하지 않음. 종료 시간이 0이하일 경우 계속 스폰.
            if (data.SpawnStartTime > GameManager.Instance.GameTime || (data.SpawnEndTime > 0 && data.SpawnEndTime < GameManager.Instance.GameTime))
                continue;

            // 스폰 횟수 초과일 경우 스폰하지 않음. 만약 스폰제한값이 0 이하 경우 제한 없음.
            if (data.SpawnMaxCount > 0 && data.SpawnMaxCount <= spawnCounter[i])
                continue;

            // 스폰 쿨타임일 경우 스폰하지 않음
            if (data.SpawnCoolTime > spawnTimers[i])
            {
                spawnTimers[i] += Time.deltaTime;
                continue;
            }

            // 스폰 쿨타임이 끝났을 경우 스폰
            spawnTimers[i] = 0f;
            ++spawnCounter[i];
            var enemy = (Enemy)PoolManager.Instance.Get(data.EnemyData.DataID);
            enemy.Init(data);
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        }
    }
}