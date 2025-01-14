using UnityEngine;

public class Magnet : MonoBehaviour
{
    CircleCollider2D magnet;
    public float MagnetStrength { get; set; } = 5f;
    public float MagnetRadius { get => magnet.radius; set => magnet.radius = value; }

    private void Awake()
    {
        if(magnet == null) magnet = GetComponent<CircleCollider2D>();
    }
}
