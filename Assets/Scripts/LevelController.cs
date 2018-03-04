using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public int level { get; private set; }
    private static LevelController levelController;
       
    private static void CreateInstanceIfNoneExists()
    {
        if (levelController == null)
        {
            GameObject go = Instantiate(new GameObject());
            go.name = "Level Controller";
            levelController = go.AddComponent<LevelController>();
        }
    }

	// Use this for initialization
	void Start () {

	}

    //Setup persistance
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void NextLevel()
    {
        level++;
        SceneManager.LoadScene("level");
    }

    private void Restart()
    {
        level = 1;
        SceneManager.LoadScene("level");
    }

    private void LoadMainMenu()
    {
        LevelController.CreateInstanceIfNoneExists();
        SceneManager.LoadScene("main");
    }

    public static void LoadNextLevel()
    {
        LevelController.CreateInstanceIfNoneExists();
        levelController.NextLevel();
    }

    public static void RestartGame()
    {
        LevelController.CreateInstanceIfNoneExists();
        levelController.Restart();
    }

    public static void GoToMainMenu()
    {
        LevelController.CreateInstanceIfNoneExists();
        levelController.LoadMainMenu();
    }

    public static int GetLevel()
    {
        return LevelController.levelController.level;
    }
}
