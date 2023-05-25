using System.Collections;
using UnityEngine;

namespace Kp4wsGames.Default
{
	public class ScreenShake : MonoBehaviour
	{
        [SerializeField] private float duration;
        [SerializeField] private float magnitude;

        public void InitiateShake()
        {
            StartCoroutine(PerformShake(duration, magnitude));
        }

        private IEnumerator PerformShake(float duration, float magnitude)
        {
            Vector3 originalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float offsetX = Random.Range(-0.5f, 0.5f) * magnitude + originalPos.x;
                float offsetY = Random.Range(-0.5f, 0.5f) * magnitude + originalPos.y;

                transform.position = new Vector3(offsetX, offsetY, originalPos.z);

                elapsedTime += Time.deltaTime;

                //Wait one frame
                yield return null;
            }

            transform.position = originalPos;
        }
    }
}