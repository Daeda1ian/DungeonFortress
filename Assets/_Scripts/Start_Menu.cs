using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    public void QuitGame() {
        Application.Quit();
    }

    public void PlayGame() {
        SceneManager.LoadScene("_Scene_0");
    }
}
