using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translation : MonoBehaviour
{
    public GameObject slider;
    public GameObject canvas;
    public GameObject displaySelected;
    public GameObject bar;
    public GameObject buttonSubmit;

    private float speed;
    private float speedMain;
    private float scrollSpeed;

    private int clampB = 35;
    private int clampE = 60;

    private int sliderMaxY = 165;
    private int sliderMinY = 35;

    private float newFov;
    private static float targetFov;
    private float velocityFov = 0.1F;
    private float offsetScalar;
    static private float smoothVar;

    static private Vector2 positionLocal;
    static private Vector2 position;
    private Vector2 velocity;
    private Vector2 touchDeltaPosition;

    private float maxX = 850, maxY = 700;

    private string MODE = "";

    private int change;

    static private bool userMove = true;
    // Use this for initialization
    void Start()
    {
        change = clampE - clampB;
        newFov = 60;
        targetFov = clampE;
        speedMain = 4.5F;
        scrollSpeed = 0.04F;

        positionLocal = new Vector2(0, 0);
        position = new Vector2(0, 0);
        velocity = new Vector2(0.1F, 0.1F);

        offsetScalar = canvas.GetComponent<RectTransform>().localScale[0];
        smoothVar = 0.025F;
        speed = speedMain;

        touchDeltaPosition = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount != 0)
        {
            MODE = "TOUCH";
        }

        if(!userMove && Vector2.Distance(position, positionLocal) < 6)
        {
            userMove = true;
            smoothVar = 0.025F;
        }

        if (userMove)
        {
            if (MODE.Equals("TOUCH"))
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (Input.touchCount > 1)
                    {
                        for(int i = 0; i < Input.touchCount; i++)
                        {
                            touchDeltaPosition += Input.GetTouch(i).deltaPosition;
                        }
                        touchDeltaPosition /= Input.touchCount;
                    }
                    else
                    {
                        touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    }
                    float x = -touchDeltaPosition.x * speed / 7 + positionLocal.x;
                    float y = -touchDeltaPosition.y * speed / 7 + positionLocal.y;
                    positionLocal = new Vector2(Mathf.Clamp(x, -maxX, maxX), Mathf.Clamp(y, -maxY, maxY));

                    if (Input.touchCount == 2)
                    {
                        Touch touchZero = Input.GetTouch(0);
                        Touch touchOne = Input.GetTouch(1);

                        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                        float newFov = targetFov += deltaMagnitudeDiff * scrollSpeed;
                        targetFov = Mathf.Clamp(newFov, clampB, clampE);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector2 touchDeltaPosition = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                    float x = -touchDeltaPosition.x * speed * 10 + positionLocal.x;
                    float y = -touchDeltaPosition.y * speed * 10 + positionLocal.y;
                    positionLocal = new Vector2(Mathf.Clamp(x, -maxX, maxX), Mathf.Clamp(y, -maxY, maxY));
                }
            }
        }

        if ((Input.touchCount > 1 || Input.GetMouseButtonDown(0)) && (!(Input.GetTouch(0).phase == TouchPhase.Moved)))
        {
            if (!highlightedButton.entered)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform)
                    {
                        hitObjChange(hit.transform.gameObject);
                    }
                }
                else
                {
                    hitObjChange(null);
                }
            }
        }

        position = Vector2.SmoothDamp(position, positionLocal, ref velocity, smoothVar);
        transform.position = new Vector3(position.x, position.y, -1000);

        speed = speedMain - (((clampE - targetFov) / change) * 2F);

        newFov = Mathf.SmoothDamp(newFov, targetFov, ref velocityFov, smoothVar);
        transform.GetComponent<Camera>().fieldOfView = newFov;
        slider.GetComponent<RectTransform>().position = new Vector3(30 * offsetScalar, ((((newFov - clampB) / change) * 130) + sliderMinY) * offsetScalar, 0);
    }

    public void camToPoint(float x, float y, float time)
    {
        userMove = false;
        smoothVar = time;
        positionLocal = new Vector2(x, y);
        targetFov = 35;
    }

    void hitObjChange(GameObject hit)
    {
        if (hit != null)
        {
            if (hit.name.Equals("GYMoutline") || hit.name.Equals("HALLoutline") || hit.name.Equals("PORTABLESoutline"))
            {
                displaySelected.GetComponentInChildren<Text>().text = hit.name.Replace("outline", "");
            }
            else
            {
                displaySelected.GetComponentInChildren<Text>().text = hit.name.Replace("outline", "") + " BUILDING";
            }

            bar.GetComponent<Animator>().Play("open");
            buttonSubmit.GetComponent<Animator>().Play("buttonAppear");
            buttonSubmit.GetComponent<Button>().onClick.AddListener(delegate
            {
                camToPoint(LoadGridObjects.storedPositions[hit][0], LoadGridObjects.storedPositions[hit][1], 0.75F);
            });
        }
        else
        {
            displaySelected.GetComponentInChildren<Text>().text = "";
            bar.GetComponent<Animator>().Play("close");
            buttonSubmit.GetComponent<Animator>().Play("buttonDisappear");
        }
    }
}