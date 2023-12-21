using UnityEngine;

public class GunStatistics : MonoBehaviour
{
    [SerializeField] public float BulletDamage = 10f;
    [SerializeField] public float BulletSpeed = 3.5f;
    [SerializeField] public float RotationSpeed = 6f;
    [SerializeField] public float BrakeRotationSpeedMod = 1f;
    [SerializeField] public float ReloadTime = 2f;
    [SerializeField] public GameObject BulletType;
    public bool IsPlayer;
}
