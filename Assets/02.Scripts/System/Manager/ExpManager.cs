using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance { get; private set; }

    [SerializeField] Data expData;
    int savedExp = 0;

    bool isReady;

    private void Awake()
    {
        Instance = this;
    }

    public void DropExp(Vector3 position, int exp)
    {
        if (GameManager.Instance.IsPause) return;
        if (GameManager.Instance.GameTime >= GameManager.Instance.EndTime) return;
        if (!isReady)
        {
            PoolManager.Instance.Add(expData, 100);
            isReady = true;
        };

        ++GameManager.Instance.Player.Kill;

        var dropExp = savedExp + exp;
        savedExp = 0;

        ExpItem expObject = (ExpItem)PoolManager.Instance.Get(expData.DataID);
        expObject.transform.position = position;
        expObject.Init(dropExp);
    }

    public void SaveExpOutsideArea(int exp)
    {
        savedExp += exp;
    }
}
