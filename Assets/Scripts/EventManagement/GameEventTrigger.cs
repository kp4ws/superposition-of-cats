using UnityEngine;
using System.Linq;
using UnityEngine.Playables;

namespace Kp4wsGames.EventManagement
{
    public class GameEventTrigger : MonoBehaviour
    {
        [SerializeField] private GameEvent triggerEvent;
        [SerializeField] private bool triggerOnce;
        [SerializeField] private string[] whoCanTrigger;
        private bool hasTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (triggerEvent == null)
                return;

            if (whoCanTrigger.Length == 0)
                return;

            if(!whoCanTrigger.Any(other.gameObject.tag.Contains))
                return;

            if (triggerOnce && hasTriggered)
                return;

            hasTriggered = true;
            triggerEvent.Raise();
        }
    }
}