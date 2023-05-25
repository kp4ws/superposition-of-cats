/*
* Copyright (c) Kp4ws
*
*/

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BDM.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        private Coroutine sceneRoutine;

        private void InitiateLoad(int sceneIndex)
        {
            if (sceneRoutine != null)
            {
                StopCoroutine(sceneRoutine);
                sceneRoutine = null;
            }

            sceneRoutine = StartCoroutine(PerformLoad(sceneIndex));
        }

        private void InitiateMainLoad(int sceneIndex)
        {
            if (sceneRoutine != null)
            {
                StopCoroutine(sceneRoutine);
                sceneRoutine = null;
            }

            sceneRoutine = StartCoroutine(PerformGameLoad(sceneIndex));
        }

        private IEnumerator PerformLoad(int sceneIndex)
        {
            yield return SceneManager.LoadSceneAsync(sceneIndex);
        }

        private IEnumerator PerformGameLoad(int sceneIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                //TODO change loading text (not implemented yet)
                if(asyncLoad.progress >= 0.9f)
                {
                    if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)//TODO make dynamic
                    {
                        asyncLoad.allowSceneActivation = true;
                        Time.timeScale = 1f;
                    }
                }
                yield return null;
            }
        }

        public void LoadScene(int sceneIndex)
        {
            InitiateLoad(sceneIndex);
        }

        public void LoadMainScene(int sceneIndex)
        {
            InitiateMainLoad(sceneIndex);
        }

        public void OpenMenu(GameObject menu)
        {
            if (menu.activeSelf)
                return;

            menu.SetActive(true);
        }

        public void CloseMenu(GameObject menu)
        {
            if (!menu.activeSelf)
                return;

            menu.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
        
    }

}