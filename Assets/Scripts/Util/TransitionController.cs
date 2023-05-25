using UnityEngine;
using UnityEngine.UI;

//Note, this effect can also be achieved through animation which I'm not currently using
public class TransitionController : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionSpeed = 2f;

    [SerializeField] private GameObject overworld;
    [SerializeField] private GameObject underworld;
    [SerializeField] private Rigidbody2D playerRigidbody;

    private bool isOverworldEnabled;
    private bool isTransitioning;

    private void Start()
    {
        isOverworldEnabled = true;
        isTransitioning = false;
        transitionImage.material.SetFloat("_Cutoff", 1);
        overworld.SetActive(true);
        underworld.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTransitioning)
        {
            isTransitioning = true;
            isOverworldEnabled = !isOverworldEnabled;
            StartCoroutine(TransitionCoroutine());
        }
    }

    private System.Collections.IEnumerator TransitionCoroutine()
    {
        // Freeze the player
        playerRigidbody.isKinematic = true;

        float targetCutoff = -0.1f - transitionImage.material.GetFloat("_Smoothing");
        float currentCutoff = transitionImage.material.GetFloat("_Cutoff");

        while (Mathf.Abs(currentCutoff - targetCutoff) > 0.01f)
        {
            currentCutoff = Mathf.MoveTowards(currentCutoff, targetCutoff, transitionSpeed * Time.deltaTime);
            transitionImage.material.SetFloat("_Cutoff", currentCutoff);
            yield return null;
        }

        if (!isOverworldEnabled)
        {
            // Disable the overworld and enable the underworld
            overworld.SetActive(false);
            underworld.SetActive(true);
        }
        else
        {
            // Enable the overworld and disable the underworld
            overworld.SetActive(true);
            underworld.SetActive(false);
        }

        targetCutoff = 1.1f;
        currentCutoff = transitionImage.material.GetFloat("_Cutoff");

        while (Mathf.Abs(currentCutoff - targetCutoff) > 0.01f)
        {
            currentCutoff = Mathf.MoveTowards(currentCutoff, targetCutoff, transitionSpeed * Time.deltaTime);
            transitionImage.material.SetFloat("_Cutoff", currentCutoff);
            yield return null;
        }

        // Transition completed
        isTransitioning = false;

        // Unfreeze the player
        playerRigidbody.isKinematic = false;
    }
}
