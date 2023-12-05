using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    [Header("Text")]
    public Text livesText;
    public Text volSliderText;

    [Header("Slider")]
    public Slider volSlider;
    

    // Start is called before the first frame update
    void Start()
    {
        if (playButton)
            playButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(1));
        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(0));

        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(UnpauseGame);

        if (settingsButton)
            settingsButton.onClick.AddListener(ShowSettingsMenu);

        if (backButton)
            backButton.onClick.AddListener(ShowMainMenu);

        if (quitButton)
            quitButton.onClick.AddListener(Quit);

        if (livesText)
        {
            GameManager.Instance.OnLivesValueChanged.AddListener((value) => UpdateLivesText(value));
            livesText.text = "Lives: " + GameManager.Instance.lives.ToString();
        }

        if (volSlider)
        {
            volSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value));
            if (volSliderText)
                volSliderText.text = volSlider.value.ToString();
        }    
    }

    void OnSliderValueChanged(float value)
    {
        volSliderText.text = value.ToString();
    }

    void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        //something else needs to be done
    }

    void UpdateLivesText(int value)
    {
        if (livesText)
            livesText.text = "Lives: " + value.ToString();
    }

    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    private void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            //hints for the lab
            if (pauseMenu.activeSelf)
            {
                //do something to pause
            }
            else
            {
                //do something to unpause
            }
        }
    }
}
