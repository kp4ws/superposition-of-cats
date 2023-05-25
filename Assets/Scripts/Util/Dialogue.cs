using UnityEngine;

namespace Kp4wsGames.Dialogue
{
    [CreateAssetMenu]
    public class Dialogue : ScriptableObject
    {
		[TextArea(10, 14)] [SerializeField] string storyText = default;
		public string GetStateStory()
		{
			return storyText;
		}
	}
}