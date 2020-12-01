using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadTeacher : MonoBehaviour
{
    List<GameObject> buttons = new List<GameObject>();
    int size = 0;

    public GameObject canvas;
    [SerializeField] GameObject button;
    public InputField inputField;

    public static string teacherValue = "";
    public static List<string> assigned;

    // Use this for initialization
    void Start()
    {
        ValueChangeCheck("");

        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, button.GetComponent<RectTransform>().sizeDelta[1] * LoadAssets.teachers.Count);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValueChangeCheck(string userInput)
    {
        size = 0;
        foreach (GameObject current in buttons)
        {
            Destroy(current);
        }
        for (int i = 0; i < LoadAssets.teachers.Count; i++)
        {
            if (LoadAssets.teachers[i].ToLower().Contains(userInput.ToLower()))
            {
                GameObject newButton = (GameObject)Instantiate(button);
                string current = LoadAssets.teachers[i];
                List<string> dat = LoadAssets.databyTeacher[current];

                newButton.transform.SetParent(canvas.transform, false);
                newButton.GetComponentInChildren<Text>().text = current;

                newButton.GetComponent<Button>().onClick.AddListener(delegate {
                    teacherValue = current;
                    assigned = dat;

                    changeScene("InformationPanel");
                });

                buttons.Add(newButton);
                size++;
            }
        }

        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, button.GetComponent<RectTransform>().sizeDelta[1] * size);

        /*
        for (int i = 0; i < rooms.Count; i++)
        {
            if (!rooms[i].Contains(userInput))
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
