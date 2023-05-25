using BDM.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TransitionControllerModded : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionSpeed = 2f;
    [SerializeField] private bool isFadeIn;
    [SerializeField] private SceneLoader sl;

    private void Start()
    {
        InitiateTransition();
    }

    public void InitiateTransition()
    {
        if (isFadeIn)
        {
            transitionImage.material.SetFloat("_Cutoff", 1);
            StartCoroutine(TransitionInCoroutine());
        }
        else
        {
            transitionImage.material.SetFloat("_Cutoff", -1f);
            StartCoroutine(TransitionOutCoroutine());
        }
    }

    private System.Collections.IEnumerator TransitionInCoroutine()
    {
        float targetCutoff = -0.1f - transitionImage.material.GetFloat("_Smoothing");
        float currentCutoff = transitionImage.material.GetFloat("_Cutoff");

        while (Mathf.Abs(currentCutoff - targetCutoff) > 0.01f)
        {
            currentCutoff = Mathf.MoveTowards(currentCutoff, targetCutoff, transitionSpeed * Time.deltaTime);
            transitionImage.material.SetFloat("_Cutoff", currentCutoff);
            yield return null;
        }

        sl?.LoadScene(1);
    }

    private System.Collections.IEnumerator TransitionOutCoroutine()
    {
        float targetCutoff = 1.1f;
        float currentCutoff = transitionImage.material.GetFloat("_Cutoff");

        while (Mathf.Abs(currentCutoff - targetCutoff) > 0.01f)
        {
            currentCutoff = Mathf.MoveTowards(currentCutoff, targetCutoff, transitionSpeed * Time.deltaTime);
            transitionImage.material.SetFloat("_Cutoff", currentCutoff);
            yield return null;
        }
    }
}