using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static string START_GAME_SCENE = "ScenePrueba";
    public static string MENU_GAME_SCENE = "Menu";
    
    public GameObject gameOverScreeen;
    public void navigateToStartGame()
    {
        SceneManager.LoadScene(START_GAME_SCENE);
        gameOverScreeen.SetActive(false);
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
