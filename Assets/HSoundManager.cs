using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class HSoundManager : MonoBehaviour
{
    public AudioClip button;
    private AudioSource source;

    private void Awake()
    {
        source=gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        var ui=FindObjectOfType<UIDocument>().rootVisualElement;

        Button button1= ui.Q<Button>("Button1");
        Button button2= ui.Q<Button>("Button2");

        if (button1!=null) button1.clicked+=()=>OnButtonClick("Forward");
        if (button2!=null) button2.clicked+=()=>OnButtonClick("GoalKeeper");
    }

    private void OnButtonClick(string sceneName)
    {
        if (button) source.PlayOneShot(button);
        StartCoroutine(LoadSceneAfterDelay(sceneName));
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
