using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private GameObject scrollingBackground;

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
            throw new System.NotSupportedException();
        } else
        {
            SceneManager.LoadScene(1);
        }
    }
}
