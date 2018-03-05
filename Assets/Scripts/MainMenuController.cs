using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private GameObject scrollingBackground;

    [SerializeField]
    private AudioClip music;

    void Start()
    {
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
}
