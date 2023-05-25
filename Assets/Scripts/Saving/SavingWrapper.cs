using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Kp4wsGames.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        //Load Save file on start
        //private IEnumerator Start()
        //{
        //    //TODO Fade out completely
        //    yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        //    //TODO Fade in
        //}

        public void OnSave(CallbackContext ctx)
        {
            if (ctx.started)
            {
                GetComponent<SavingSystem>().Save(defaultSaveFile);
            }
        }

        public void OnLoad(CallbackContext ctx)
        {
            if (ctx.started)
            {
                GetComponent<SavingSystem>().Load(defaultSaveFile);
            }
        }
    }
}