using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadGridObjects : MonoBehaviour
{
    public GameObject preFabSprite;
    public GameObject buttonPlaced;
    public GameObject arrayHolder;

    public static Dictionary<GameObject, float[]> storedPositions = new Dictionary<GameObject, float[]>();

    private GameObject selectedBuilding;

    // Use this for initialization
    void Start()
    {
        selectedBuilding = null;

        float scalar = (6600 / 2200) * 10;
        //Load Objects
        foreach(string buildin in LoadAssets.buildings)
        {
            float x = LoadAssets.buildingPos[buildin][0] - (2200 / 2f), 
                y = (1700 / 2f) - LoadAssets.buildingPos[buildin][1], 
                ang = -LoadAssets.buildingPos[buildin][2];

            GameObject buildingObj = Instantiate(preFabSprite) as GameObject;
            buildingObj.GetComponent<SpriteRenderer>().sprite = Resources.Load(buildin, typeof(Sprite)) as Sprite;
            buildingObj.GetComponent<SpriteRenderer>().color = new Color(235, 235, 235);
            buildingObj.GetComponent<Transform>().position = new Vector2(x, y);
            buildingObj.GetComponent<Transform>().localScale = new Vector3(scalar, scalar, 1);
            buildingObj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, ang);
            buildingObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            buildingObj.name = buildin;
            buildingObj.transform.SetParent(arrayHolder.transform);

            GameObject buildingBody = Instantiate(preFabSprite) as GameObject;
            buildingBody.GetComponent<SpriteRenderer>().sprite = Resources.Load(buildin + "W", typeof(Sprite)) as Sprite;
            buildingBody.GetComponent<SpriteRenderer>().color = new Color(235, 235, 235);
            //buildingBody.GetComponent<Button>().image.sprite = Resources.Load(buildin + "W", typeof(Sprite)) as Sprite;
            buildingBody.GetComponent<Transform>().position = new Vector2(x, y);
            buildingBody.GetComponent<Transform>().localScale = new Vector3(scalar, scalar, 1);
            buildingBody.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, ang);
            buildingBody.GetComponent<SpriteRenderer>().sortingOrder = 1;
            buildingBody.name = buildin + "outline";
            buildingBody.transform.SetParent(arrayHolder.transform);

            buildingBody.AddComponent<BoxCollider>();
            buildingBody.AddComponent<Animator>();

            float[] pos = new float[2];
            pos[0] = x;
            pos[1] = y;
            storedPositions.Add(buildingBody, pos);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
