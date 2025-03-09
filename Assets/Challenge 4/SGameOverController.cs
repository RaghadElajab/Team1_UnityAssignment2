using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.SceneManagement;
public class SGameOverController : MonoBehaviour

{
    public UIDocument gameOverUI;
    public SpawnManagerX spawner;
    private static VisualElement gameOverContainer;
    private static Button restartButton;
    private static Button homeButton;
    private static Label HighScore;
    void Awake()
    {
        gameOverContainer = gameOverUI.rootVisualElement.Q<VisualElement>("VisualElement");
        HighScore = gameOverContainer.Q<Label>("HighScore");
        restartButton = gameOverContainer.Q<Button>("PlayAgain");
        homeButton = gameOverContainer.Q<Button>("HomePage");

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        homeButton.clicked += goHome;
        restartButton.clicked += restartGame;
        gameOverContainer.style.display = DisplayStyle.None;
        gameOverContainer.SetEnabled(false);
    }

    public void showGameOver(int highscore)
    {
        gameOverContainer.style.display = DisplayStyle.Flex;
        gameOverContainer.SetEnabled(true);
        gameOverContainer.style.opacity = 1;
        HighScore.text = "" + highscore;

        Time.timeScale = 0;

    }
    
    public void restartGame()
    {
        Time.timeScale = 1;
        spawner.reset();

    }

    public void goHome()
    {
        spawner.resetScore();
        Time.timeScale = 1;
        SceneManager.LoadScene("Homescreen");
    }
}
