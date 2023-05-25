using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kp4wsGames.Saving
{
	//Class will be updated both in play time and edit time
	[ExecuteAlways]
	public class SaveableEntity : MonoBehaviour
	{
		[SerializeField] private string uniqueIdentifier = "";
		private static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

		public string GetUniqueIdentifier() //For player, should manually update the UUID to player
        {
			return uniqueIdentifier;
        }

		public object CaptureState()
        {
			Dictionary<string, object> state = new Dictionary<string, object>();
			foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
				state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
			return state;
        }

		public void RestoreState(object state)
        {
			Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
			foreach (ISaveable saveable in GetComponents<ISaveable>())
			{
				string typeString = saveable.GetType().ToString();
				if(stateDict.ContainsKey(typeString))
                {
					saveable.RestoreState(stateDict[typeString]);
                }
			}
		}
#if UNITY_EDITOR
		private void Update()
        {
			//UUIDs are saved in scenes (not prefabs)

			if (Application.IsPlaying(gameObject))
				return;
			if (string.IsNullOrEmpty(gameObject.scene.path)) //If in prefab mode, return
				return;

			SerializedObject serializedObject = new SerializedObject(this);
			SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

			if(string.IsNullOrEmpty(property.stringValue) && !IsUnique(property.stringValue))
            {
				property.stringValue = System.Guid.NewGuid().ToString();
				serializedObject.ApplyModifiedProperties();
            }
			globalLookup[property.stringValue] = this;
		}
#endif

		private bool IsUnique(string candidate)
        {
			if (!globalLookup.ContainsKey(candidate))
				return true;
			if(globalLookup[candidate] == this)
				return true;
			
			if (globalLookup[candidate] == null)
			{
				globalLookup.Remove(candidate);
				return true;
			}

			if(globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
				globalLookup.Remove(candidate);
				return true;
			}

			return false;
        }
    }
}