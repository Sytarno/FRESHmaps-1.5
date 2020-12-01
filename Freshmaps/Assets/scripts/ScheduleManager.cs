using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScheduleManager : MonoBehaviour {
    public GameObject verticalLayoutGroup;
    public GameObject preFabClass;
    public GameObject text;
    public GameObject hideInactive;

    private GameObject hideClasses;
    private List<GameObject> classButtons = new List<GameObject>();

    //manual input
    private bool toggled = false;

	// Use this for initialization
	void Start () {
        for(int i = 0; i < 7; i++)
        {
            LoadAssets.AddClass("NONE", LoadAssets.studentClasses.Count + 1, "", "");
        }
        updateClasses();

        /**
        addButton.GetComponent<Button>().onClick.AddListener(delegate {
            LoadAssets.AddClass("NONE", LoadAssets.studentClasses.Count+1, "", "");
            updateClasses();
        });
        **/

        hideClasses = (GameObject)Instantiate(hideInactive);
        hideClasses.transform.SetParent(verticalLayoutGroup.transform);
        hideClasses.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        hideClasses.GetComponent<Button>().onClick.AddListener(delegate { toggleInactive(); });
	}

    public void updateClasses()
    {
        foreach(GameObject c in classButtons)
        {
            Destroy(c);
        }

        classButtons = new List<GameObject>();
        for (int i = 0; i < LoadAssets.studentClasses.Count; i++)
        {
            Room currentRoom = LoadAssets.studentClasses[i];

            GameObject classObj = (GameObject)Instantiate(preFabClass);
            classObj.name = i + "";

            GameObject separateBar = classObj.transform.Find("Image").gameObject;
            //ColorBlock set = separateBar.GetComponent<Button>().colors;
            //set.normalColor = colors[i] + new Color(20/255F, 20/255F, 20/255F);
            separateBar.GetComponent<Image>().color = LoadAssets.colorsB[i] + new Color(10 / 255F, 10 / 255F, 10 / 255F);

            separateBar.GetComponentInChildren<Text>().text = currentRoom.roomPeriod + "";

            GameObject mainInfo = classObj.transform.Find("displayInfo").gameObject;
            Text info = mainInfo.GetComponent<Text>();
            info.text = currentRoom.roomBuilding + currentRoom.roomNumber + (currentRoom.roomTeacher.Equals("NONE") ? "" : " \t") + currentRoom.roomTeacher.ToUpper();

            classObj.transform.SetParent(verticalLayoutGroup.transform);
            classObj.transform.SetSiblingIndex(currentRoom.roomPeriod-1);
            classObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            classObj.GetComponent<Button>().onClick.AddListener(delegate {
                LoadAssets.modifiedRoom = currentRoom;
                SceneManager.LoadScene("EditPanel");
            });
                
            classButtons.Add(classObj);
        }
    }

    public void toggleInactive()
    {
        toggled = !toggled;
        if (toggled == true)
        {
            for(int i = 0; i < LoadAssets.studentClasses.Count; i++)
            {
                if (LoadAssets.studentClasses[i].roomTeacher.Equals("NONE"))
                {
                    Destroy(classButtons[i]);
                }
            }
        }
        else
        {
            updateClasses();
        }

        hideClasses.GetComponentInChildren<Text>().text = toggled ? "SHOW INACTIVE PERIODS" : "HIDE INACTIVE PERIODS";
    }
	// Update is called once per frame
	void Update () {
		
	}
}
