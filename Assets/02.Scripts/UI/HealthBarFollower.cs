using UnityEngine;

/// <summary>
/// HP UI ��ġ ���� �Լ�
/// </summary>
public class HealthBarFollower : MonoBehaviour
{
    RectTransform rect;
    Camera _camera;
    [SerializeField] Transform target;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        _camera = Camera.main;
        if(target == null) target = GameManager.Instance.Player.transform;
    }


    void LateUpdate()
    {   // HP ��ġ ������
        rect.position = _camera.WorldToScreenPoint(target.position);
    }
}
