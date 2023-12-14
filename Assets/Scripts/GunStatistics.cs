using UnityEngine;

public class GunStatistics : MonoBehaviour
{
    [SerializeField] public float BulletDamage = 10f;
    [SerializeField] public float BulletSpeed = 2f;
    [SerializeField] public float RotationSpeed = 4f;
    [SerializeField] public float BrakeRotationSpeedMod = 1f;
    [SerializeField] public GameObject BulletType;
    public bool IsPlayer;
}
