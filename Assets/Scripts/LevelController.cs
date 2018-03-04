using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public int level { get; private set; }
    public static LevelController levelController { get; set; }

	// Use this for initialization
	void Start () {
        level = 1;
	}

    //Setup persistance
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void NextLevel()
    {
        level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadNextLevel()
    {
        levelController.NextLevel();
    }
}
