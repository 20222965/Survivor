using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Data")]
public class Data : ScriptableObject, IPoolData
{

    [Header("Data Info")]
    [Tooltip("������ ����")]
    [SerializeField] private IPoolData.DataType _type;
    /// <summary>
    /// ������ ����
    /// </summary>
    public IPoolData.DataType Type { get => _type; }


    [Tooltip("�ߺ����� �ʴ� ������ ID")]
    [SerializeField] private int _dataID;
    public int DataID { get => _dataID; }


    [Tooltip("������ �̸�")]
    [SerializeField] private string _dataName;
    public string DataName {  get => _dataName; }


    [Tooltip("������ ������")]
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab { get => _prefab; }

}
