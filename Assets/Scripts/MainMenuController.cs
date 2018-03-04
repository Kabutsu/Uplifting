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
        GameObject levelControllerGo = Instantiate(new GameObject());
        levelControllerGo.name = "Level Controller";
        levelControllerGo.AddComponent<LevelController>();
        LevelController.levelController = levelControllerGo.GetComponent<LevelController>();


        if (includeTutorial)
        {
            throw new System.NotSupportedException();
        } else
        {
            SceneManager.LoadScene(1);
        }
    }
}
