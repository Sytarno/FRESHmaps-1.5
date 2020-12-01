using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room{
    public string roomTeacher;
    public string roomNumber;
    public string roomBuilding;
    public int roomPeriod;

	public Room(string teacher, string number, string building, int period)
    {
        roomTeacher = teacher;
        roomNumber = number;
        roomBuilding = building;
        roomPeriod = period;
    }
}
