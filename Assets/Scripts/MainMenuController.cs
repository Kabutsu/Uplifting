using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private GameObject scrollingBackground;

    [SerializeField]
    private AudioClip music;

    [SerializeField]
    private GameObject titleBox;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject tutorialButton;
    [SerializeField]
    private GameObject creditsButton;
    [SerializeField]
    private GameObject creditsPanel;

    void Start()
    {
        creditsPanel.SetActive(false);
        MusicController.InstaniateIfNoInstance(music);
    }

    // Update is called once per frame
    void Update() {
        scrollingBackground.transform.Translate(Vector3.down * Time.deltaTime);
    }

    public void StartGame()
    {
        StartGame(false);
    }

    public void StartTutorial()
    {
        StartGame(true);
    }

    private void StartGame(bool includeTutorial)
    {
        if (includeTutorial)
        {
            SceneManager.LoadScene("Tutorial");
        } else
        {
            LevelController.RestartGame();
        }
    }

    public void ShowCredits()
    {
        titleBox.SetActive(false);
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        creditsButton.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        titleBox.SetActive(true);
        startButton.SetActive(true);
        tutorialButton.SetActive(true);
        creditsButton.SetActive(true);
        creditsPanel.SetActive(false);
    }
}
