using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadAssets : MonoBehaviour {

    public TextAsset gridData;
    public TextAsset TRData;

    //GRID
    public static List<string> buildings;
    public static Dictionary<string, float[]> buildingPos;

    public static List<string> roomIDs;
    public static Dictionary<string, float[]> roomPos;

    //TEACHER/ROOM
    public static List<string> rooms = new List<string>();
    public static List<string> teachers = new List<string>();
    public static Dictionary<string, List<string>> databyTeacher = new Dictionary<string, List<string>>();
    public static Dictionary<string, List<string>> databyRoom = new Dictionary<string, List<string>>();

    //SAVE MODE
    public static bool LOADED = false;

    //SAVE SCHEDULE
    public static List<Room> studentClasses = new List<Room>();

    //MANUAL
    public List<int[]> colors = new List<int[]>();

    //PASSED ARGUMENT
    public static Room modifiedRoom = null;
    public static Room newSelectedRoom = null;

    public static bool replaceSearch = false;

    //COLORDATA
    public static List<Color> colorsB = new List<Color>();

    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.orientation = ScreenOrientation.Portrait;

        colorsB.Add(new Color(229 / 255.0F, 115 / 255.0F, 115 / 255.0F));
        colorsB.Add(new Color(186 / 255.0F, 104 / 255.0F, 200 / 255.0F));
        colorsB.Add(new Color(121 / 255.0F, 134 / 255.0F, 203 / 255.0F));
        colorsB.Add(new Color(79 / 255.0F, 195 / 255.0F, 247 / 255.0F));
        colorsB.Add(new Color(77 / 255.0F, 182 / 255.0F, 172 / 255.0F));
        colorsB.Add(new Color(174 / 255.0F, 213 / 255.0F, 129 / 255.0F));
        colorsB.Add(new Color(255 / 255.0F, 183 / 255.0F, 77 / 255.0F));

        //DontDestroyOnLoad(this);
        loadFiles();

        LOADED = true;
    }

    public void loadFiles()
    {
        //LOAD GRID
        StreamReader input = new StreamReader(new MemoryStream(gridData.bytes));
        string currentType = "";

        buildings = new List<string>();
        buildingPos = new Dictionary<string, float[]>();

        //Parse Data
        using (input)
        {
            string IMGDATA = input.ReadLine();
            string currentLine = "";

            try
            {
                do
                {
                    currentLine = input.ReadLine();
                    if (currentLine.Contains("/"))
                    {
                        currentType = currentLine.Substring(1, currentLine.Length - 1);
                    }

                    if (currentType.Equals("BUILDING") && !currentLine.Equals("/BUILDING"))
                    {
                        string build = currentLine.Split(' ')[0], remain = currentLine.Split(' ')[1];
                        buildings.Add(build);

                        float[] pos = new float[3];
                        pos[0] = float.Parse(remain.Split('\t')[0]);
                        pos[1] = float.Parse(remain.Split('\t')[1]);
                        pos[2] = float.Parse(remain.Split('\t')[2]);

                        buildingPos.Add(build, pos);
                    }
                    else if(currentType.Equals("ROOM") && !currentLine.Equals("/ROOM"))
                    {
                        string id = currentLine.Split(' ')[0], remain = currentLine.Split(' ')[1];
                        roomIDs.Add(id);

                        float[] pos = new float[3];
                        pos[0] = float.Parse(remain.Split('\t')[0]);
                        pos[1] = float.Parse(remain.Split('\t')[1]);
                        pos[2] = float.Parse(remain.Split('\t')[2]);

                        roomPos.Add(id, pos);
                    }

                } while (currentLine != "" || currentLine != null);
                input.Close();
            }
            catch { }
        }





        //LOAD TEACHER/ROOM
        StreamReader inputG = new StreamReader(new MemoryStream(TRData.bytes));

        using (inputG)
        {
            string currentLine = "";
            string Teacher = "";
            string RoomRaw = "";

            try
            {
                do
                {
                    currentLine = inputG.ReadLine();
                    Teacher = currentLine.Split('\t')[0];
                    RoomRaw = currentLine.Split('\t')[1];

                    string[] Room = RoomRaw.Split(',');
                    List<string> activeRooms = new List<string>();

                    foreach (string c in Room)
                    {
                        activeRooms.Add(c);
                        if (!rooms.Contains(c))
                        {
                            rooms.Add(c);
                        }
                    }

                    teachers.Add(Teacher);

                    //array for teacher-room
                    databyTeacher.Add(Teacher, activeRooms);

                    //array for room-teacher
                    foreach (string roomId in activeRooms)
                    {
                        if (databyRoom.ContainsKey(roomId))
                        {
                            databyRoom[roomId].Add(Teacher);
                        }
                        else
                        {
                            List<string> currTeacher = new List<string>();
                            currTeacher.Add(Teacher);
                            databyRoom.Add(roomId, currTeacher);
                        }
                    }
                } while (currentLine != "" || currentLine != null);
                inputG.Close();
            }
            catch { }

            teachers.Sort();

            rooms.Sort();
            rooms.RemoveAt(0);
        }

    }

    public static void AddClass(string currentTeacher, int period, string room, string building)
    {
        if (studentClasses.Count == 0)
        {
            studentClasses.Add(new Room(currentTeacher, room, building, period));
        }
        else
        {
            if (studentClasses.Count < 7)
            {
                studentClasses.Insert(period - 1, new Room(currentTeacher, room, building, period));
            }
        }

        SaveSchedule();
    }

    public static void EditClass(string newTeacher, int period, string newRoom, string newBuilding)
    {
        studentClasses[period - 1].roomBuilding = newBuilding;
        studentClasses[period - 1].roomTeacher = newTeacher;
        studentClasses[period - 1].roomNumber = newRoom;

        SaveSchedule();
    }

    public static void SaveSchedule()
    {
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
