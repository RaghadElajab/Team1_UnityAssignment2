using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HomePageHandler : MonoBehaviour
{
    public UIDocument UIDoc;
    private Button forward;
    private Button gkeeper;
    private VisualElement screen;
    public AudioClip button;
    public GameObject hSoundMg;
    private AudioSource audioSource;
    void Start()
    {
        Time.timeScale=0;
        forward=UIDoc.rootVisualElement.Q<Button>("forward");
        gkeeper=UIDoc.rootVisualElement.Q<Button>("Gkeeper");
        screen=UIDoc.rootVisualElement.Q<VisualElement>("VisualElement");
        audioSource= hSoundMg.GetComponent<AudioSource>();
        forward.clicked+=ForwardMode;
        gkeeper.clicked+=GkeeperMode;
    }
    void ForwardMode()
    {
        audioSource.PlayOneShot(button);
        LoadGameScene("Challenge 4");
    }
    void GkeeperMode()
    {
        audioSource.PlayOneShot(button);
        LoadGameScene("GkeeperMode");
    }
    void LoadGameScene(string sceneName)
    {
        Time.timeScale=1;
        SceneManager.LoadScene(sceneName);
    }
}
