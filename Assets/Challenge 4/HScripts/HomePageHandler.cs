using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HomePageHandler : MonoBehaviour
{
    public UIDocument UIDoc;
    private Button forward;
    private Button gkeeper;
    private VisualElement screen;

    void Start()
    {
        Time.timeScale = 0;
        forward = UIDoc.rootVisualElement.Q<Button>("forward");
        gkeeper = UIDoc.rootVisualElement.Q<Button>("Gkeeper");
        screen = UIDoc.rootVisualElement.Q<VisualElement>("VisualElement");
        forward.clicked += ForwardMode;
        gkeeper.clicked += GkeeperMode;
    }

    void ForwardMode()
    {
        LoadGameScene("ForwardScene");
    }

    void GkeeperMode()
    {
        LoadGameScene("GkeeperMode");
    }

    void LoadGameScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}