using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditSequence : MonoBehaviour {
    private Room selectedClass;

    public GameObject currentClassButton;
    public GameObject newClassButton;
    public GameObject horizontalGroup;
    public GameObject cancel;
    public GameObject periodDisplay;

    private Text currentText;
    private Text newText;

	// Use this for initialization
	void Start () {
        if (LoadAssets.modifiedRoom != null)
        {
            selectedClass = LoadAssets.modifiedRoom;
        }

        ColorBlock set = cancel.GetComponent<Button>().colors;
        set.normalColor = LoadAssets.colorsB[selectedClass.roomPeriod - 1];
        cancel.GetComponent<Button>().colors = set;

        currentClassButton.GetComponent<Button>().enabled = false;

        currentText = currentClassButton.GetComponentInChildren<Text>();
        currentText.text = selectedClass.roomTeacher + (selectedClass.roomTeacher.Equals("NONE") ? "" : "\n") + selectedClass.roomBuilding + selectedClass.roomNumber;

        newText = newClassButton.GetComponentInChildren<Text>();
        newText.text = "SELECT NEW CLASS";

        cancel.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("BellSchedule"); });
        periodDisplay.GetComponent<Text>().text = "Period " + selectedClass.roomPeriod;

        newClassButton.GetComponent<Button>().onClick.AddListener(delegate {
            LoadAssets.replaceSearch = true;
            SceneManager.LoadScene("SearchOptions");
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
