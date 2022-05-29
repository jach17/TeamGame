using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static string START_GAME_SCENE = "ScenePrueba";
    public static string MENU_GAME_SCENE = "Menu";
    public void navigateToStartGame()
    {
        SceneManager.LoadScene(START_GAME_SCENE);
    }
    public void navigateToMenu()
    {
        SceneManager.LoadScene(MENU_GAME_SCENE);
    }
    public void exitGame()
    {
        print("Exit game");
    }
}
