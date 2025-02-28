using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class HSoundManager : MonoBehaviour
{
    public AudioClip buttonClickSound;
    public float delayBeforeSceneLoad = 2f; // Small delay to let the sound play
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        var ui = FindObjectOfType<UIDocument>().rootVisualElement;

        Button button1 = ui.Q<Button>("Button1");
        Button button2 = ui.Q<Button>("Button2");

        if (button1 != null) button1.clicked += () => OnButtonClick("Forward");
        if (button2 != null) button2.clicked += () => OnButtonClick("GoalKeeper");
    }

    private void OnButtonClick(string sceneName)
    {
        if (buttonClickSound) audioSource.PlayOneShot(buttonClickSound);
        StartCoroutine(LoadSceneAfterDelay(sceneName));
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
