using System.Collections.Generic;
using UnityEngine;

namespace Kp4wsGames.Config
{
    public class GameConfig : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
