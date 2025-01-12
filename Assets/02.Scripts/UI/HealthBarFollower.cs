using UnityEngine;

/// <summary>
/// HP UI 위치 조정 함수
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
    {   // HP 위치 재조정
        rect.position = _camera.WorldToScreenPoint(target.position);
    }
}
