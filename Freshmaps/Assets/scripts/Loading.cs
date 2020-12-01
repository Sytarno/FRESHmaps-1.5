using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Loading : MonoBehaviour {

    public GameObject loadBar;

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadAsynchronously("Menu"));
	}

    IEnumerator LoadAsynchronously(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");

        while (!operation.isDone)
        {
            loadBar.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * operation.progress, 100);
            yield return null;
        }
    }
}
