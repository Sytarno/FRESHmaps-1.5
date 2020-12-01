using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondaryInfo : MonoBehaviour {
    public GameObject display;
    public GameObject verticalGroup;
    public GameObject returnButton;
    public GameObject preFab;
    public GameObject finalButton;
    public GameObject selected;

    private string chosen;
    private List<string> extra;
    private string finalRoom;
    private string finalTeacher;

    // Use this for initialization
    void Start () {
        chosen = "";
        finalRoom = "";
        finalTeacher = "";

        if (LoadTeacher.teacherValue != "")
        {
            chosen = LoadTeacher.teacherValue;
            extra = LoadTeacher.assigned;

            display.GetComponent<Text>().fontSize = 25;
            display.GetComponent<Text>().text = chosen;

            returnButton.GetComponent<Button>().onClick.AddListener(delegate {
                LoadTeacher.teacherValue = "";
                SceneManager.LoadScene("SearchTeacher");
            });

            verticalGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(0, preFab.GetComponent<RectTransform>().sizeDelta[1] * extra.Count);

            if(extra.Count == 1)
            {
                finalRoom = extra[0];
                updateR();

                GameObject newButton = (GameObject)Instantiate(preFab);
                newButton.transform.SetParent(verticalGroup.transform, false);
                newButton.GetComponentInChildren<Text>().text = extra[0];
                newButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                for (int i = 0; i < extra.Count; i++)
                {
                    GameObject newButton = (GameObject)Instantiate(preFab);
                    string current = extra[i];

                    newButton.transform.SetParent(verticalGroup.transform, false);
                    newButton.GetComponentInChildren<Text>().text = current;

                    newButton.GetComponent<Button>().onClick.AddListener(delegate {
                        finalRoom = current;
                        updateR();
                    });
                }
            }
        }else if(LoadRoom.roomValue != "")
        {
            chosen = LoadRoom.roomValue;
            extra = LoadRoom.assigned;

            display.GetComponent<Text>().fontSize = 50;
            display.GetComponent<Text>().text = chosen;

            returnButton.GetComponent<Button>().onClick.AddListener(delegate {
                LoadRoom.roomValue = "";
                SceneManager.LoadScene("SearchRoom");
            });

            verticalGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(0, preFab.GetComponent<RectTransform>().sizeDelta[1] * extra.Count);

            if (extra.Count == 1)
            {
                finalTeacher = extra[0];
                updateT();

                GameObject newButton = (GameObject)Instantiate(preFab);
                newButton.transform.SetParent(verticalGroup.transform, false);
                newButton.GetComponentInChildren<Text>().text = extra[0];
                newButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                Debug.Log("test");
                for (int i = 0; i < extra.Count; i++)
                {
                    GameObject newButton = (GameObject)Instantiate(preFab);
                    string current = extra[i];

                    newButton.transform.SetParent(verticalGroup.transform, false);
                    newButton.GetComponentInChildren<Text>().text = current;

                    newButton.GetComponent<Button>().onClick.AddListener(delegate {
                        finalTeacher = current;
                        updateT();
                    });
                }
            }
        }
	}
	
    public void updateT()
    {
        selected.GetComponentInChildren<Text>().text = "SELECTED TEACHER: " + finalTeacher;
        setSubmit();
    }

    public void updateR()
    {
        selected.GetComponentInChildren<Text>().text = "SELECTED ROOM: " + finalRoom;
        setSubmit();
    }

    public void setSubmit()
    {
        if (LoadAssets.replaceSearch)
        {
            if (finalRoom != "")
            {
                finalButton.GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (finalRoom.Equals("GYM"))
                    {
                        LoadAssets.EditClass(chosen, LoadAssets.modifiedRoom.roomPeriod, "", finalRoom);
                    }
                    else
                    {
                        LoadAssets.EditClass(chosen, LoadAssets.modifiedRoom.roomPeriod, finalRoom.Substring(1, finalRoom.Length - 1), finalRoom.Substring(0, 1));
                    }
                    LoadAssets.replaceSearch = false;
                    SceneManager.LoadScene("EditPanel");
                });
            }else if(finalTeacher != "")
            {
                finalButton.GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (chosen.Equals("GYM"))
                    {
                        LoadAssets.EditClass(finalTeacher, LoadAssets.modifiedRoom.roomPeriod, "", chosen);
                    }
                    else
                    {
                        LoadAssets.EditClass(finalTeacher, LoadAssets.modifiedRoom.roomPeriod, chosen.Substring(1, chosen.Length - 1), chosen.Substring(0, 1));
                    }
                    LoadAssets.replaceSearch = false;
                    SceneManager.LoadScene("EditPanel");
                });
            }
        }
    }

	// Update is called once per frame
	void Update () {
 
    }
}
