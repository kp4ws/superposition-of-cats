using UnityEngine;
using System.Linq;
using UnityEngine.Playables;
using UnityEngine.Events;

namespace Kp4wsGames.EventManagement
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private bool triggerOnce = true;

        //An alternative to this could be a list of game objects?
        [SerializeField] private string[] whoCanTrigger;
        [SerializeField] private UnityEvent startEvent;
        [SerializeField] private UnityEvent endEvent;

        private bool hasTriggered;

        private PlayableDirector director;

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
        }

        private void OnEnable()
        {
            director.played += BeginCutscene;
            director.stopped += EndCutscene;
        }

        private void OnDisable()
        {
            director.played -= BeginCutscene;
            director.stopped -= EndCutscene;
        }

        private void BeginCutscene(PlayableDirector pd)
        {
            startEvent.Invoke();
        }

        private void EndCutscene(PlayableDirector pd)
        {
            endEvent.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (whoCanTrigger.Length == 0)
                return;

            if (!whoCanTrigger.Any(other.gameObject.tag.Contains))
                return;

            if (triggerOnce && hasTriggered)
                return;

            hasTriggered = true;
            GetComponent<PlayableDirector>().Play();
        }
    }
}