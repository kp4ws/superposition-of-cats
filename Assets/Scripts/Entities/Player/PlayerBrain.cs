using UnityEngine;

namespace Kp4wsGames.Player
{
    [CreateAssetMenu]
    public class PlayerBrain : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RunSpeed { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float GravityValue { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }
        [field: SerializeField] public float MinThreshold { get; private set; }
    }
}