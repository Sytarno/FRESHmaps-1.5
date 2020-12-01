using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetReturn : MonoBehaviour {
    public GameObject returnButton;
    public GameObject verticalGroup;
    public GameObject buttonClone;

	// Use this for initialization
	void Start () {
        returnButton.GetComponent<Button>().onClick.AddListener(delegate {
            if (LoadAssets.replaceSearch)
            {
                SceneManager.LoadScene("EditPanel");
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        });

        if (LoadAssets.replaceSearch && !LoadAssets.modifiedRoom.roomTeacher.Equals("NONE"))
        {
            GameObject NONE = (GameObject)Instantiate(buttonClone);
            NONE.transform.SetParent(verticalGroup.transform);
            NONE.GetComponentInChildren<Text>().text = "REMOVE CLASS";
            NONE.GetComponent<RectTransform>().localScale = new Vector3(0.89167F, 0.89167F, 1);
            NONE.GetComponent<Button>().onClick.AddListener(delegate
            {
                LoadAssets.EditClass("NONE", LoadAssets.modifiedRoom.roomPeriod, "", "");
                LoadAssets.replaceSearch = false;
                SceneManager.LoadScene("EditPanel");
            });
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
