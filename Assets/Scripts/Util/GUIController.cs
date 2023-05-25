using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject introMenu;
    [SerializeField] GameObject creditMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject transitionScreen;

    public void ActivateTransition()
    {
        transitionScreen.SetActive(true);
    }

    public void ActivateIntroMenu()
    {
        introMenu?.SetActive(true);
    }

    public void DeactivateIntroMenu()
    {
        introMenu?.SetActive(false);
    }

    public void ActivateMainMenu()
    {
        //mainMenu?.SetActive(true);
        mainMenu.GetComponent<Animator>().SetTrigger("InCredits");
    }

    public void DeactivateMainMenu()
    {
        mainMenu?.SetActive(false);
    }

    public void ActivateCreditMenu()
    {
        creditMenu?.SetActive(true);
    }

    public void DeactivateCreditMenu()
    {
        creditMenu?.SetActive(false);
    }
}