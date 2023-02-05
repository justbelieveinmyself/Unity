using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSCR : MonoBehaviour
{
    private int _buildIndexSceneGame = 1;
    private int _buildIndexMenuGame = 0;
    private int _buildSettingGame = 2;
    private void Awake() {
        Cursor.lockState = CursorLockMode.None;
    }
    public void start()
    {
        SceneManager.LoadScene(_buildIndexSceneGame);
    }

    public void back()
    {
        SceneManager.LoadScene(_buildIndexMenuGame);
    }

    public void settings()
    {
        SceneManager.LoadScene(_buildSettingGame);
    }

    public void exit()
    {
        Debug.Log("Game exit");
        Application.Quit();
    }
}
