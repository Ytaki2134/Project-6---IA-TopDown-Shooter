using UnityEngine;

public class TankStatistics : MonoBehaviour
{
    [SerializeField] public GameObject Parent;
    [SerializeField] public float Speed = 15f;
    [SerializeField] public float BrakeSpeedMod = 0.9f;

    [SerializeField] public float RotationSpeed = 2.5f;
    [SerializeField] public float BrakeRotationSpeedMod = 1.5f;

    [SerializeField] public float Health = 20f;


}
