using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Data")]
public class Data : ScriptableObject, IPoolData
{

    [Header("Data Info")]
    [Tooltip("데이터 종류")]
    [SerializeField] private IPoolData.DataType _type;
    /// <summary>
    /// 데이터 종류
    /// </summary>
    public IPoolData.DataType Type { get => _type; }


    [Tooltip("중복되지 않는 데이터 ID")]
    [SerializeField] private int _dataID;
    public int DataID { get => _dataID; }


    [Tooltip("데이터 이름")]
    [SerializeField] private string _dataName;
    public string DataName {  get => _dataName; }


    [Tooltip("데이터 프리팹")]
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab { get => _prefab; }

}
