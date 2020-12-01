using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadRoom : MonoBehaviour {
    List<GameObject> buttons = new List<GameObject>();
    int size = 0;

    public GameObject canvas;
    [SerializeField] GameObject button;
    public InputField inputField;

    public static string roomValue = "";
    public static List<string> assigned;

    // Use this for initialization
    void Start()
    {

        ValueChangeCheck("");

        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, button.GetComponent<RectTransform>().sizeDelta[1] * LoadAssets.rooms.Count);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ValueChangeCheck(string userInput)
    {
        size = 0;
        foreach (GameObject current in buttons)
        {
            Destroy(current);
        }
        for (int i = 0; i < LoadAssets.rooms.Count; i++)
        {
            if (LoadAssets.rooms[i].ToLower().Contains(userInput.ToLower()))
            {
                GameObject newButton = (GameObject)Instantiate(button);
                string current = LoadAssets.rooms[i];
                List<string> dat = LoadAssets.databyRoom[current];

                newButton.transform.SetParent(canvas.transform, false);
                newButton.GetComponentInChildren<Text>().text = current;
               
                    newButton.GetComponent<Button>().onClick.AddListener(delegate {
                        roomValue = current;
                        assigned = dat;

                        changeScene("InformationPanel");
                    });
             
                buttons.Add(newButton);
                size++;
            }
        }

        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, button.GetComponent<RectTransform>().sizeDelta[1] * size);

        /*
        for (int i = 0; i < LoadAssets.rooms.Count; i++)
        {
            if (!LoadAssets.rooms[i].Contains(userInput))
            {
                Destroy(buttons[i]);
            }
        }
        */
    }

    public void changeScene(string a)
    {
        SceneManager.LoadScene(a);
    }
}
