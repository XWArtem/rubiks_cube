using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();

    public static bool AutoRotating;
    public static bool Started;
    //public static bool IsSolving;
    //public static bool Solved
    //{
    //    get
    //    {
    //        return Solved;
    //    }
    //    set
    //    {
    //        if (value == true)
    //        {
    //            print("Solved!");
    //        }
    //        Solved = value;
    //    }
    //}

    public void PickUp(List<GameObject> cubeSide)
    {
        foreach (GameObject face in cubeSide)
        {
            if (face != cubeSide[4])
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
        cubeSide[4].transform.parent.GetComponent<PivotRotation>().RotateSide(cubeSide);
    }

    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        foreach (GameObject littleCube in littleCubes)
        {
            if (littleCube != littleCubes[4])
            {
                littleCube.transform.parent.transform.parent = pivot;
            }
        }
    }

    private string GetSideString(List<GameObject> side)
    {
        string sideString = "";
        foreach (GameObject face in side)
        {
            sideString += face.name[0].ToString();
        }
        return sideString;
    }

    public string GetStateString()
    {
        string stateString = "";
        stateString += GetSideString(up);
        stateString += GetSideString(right);
        stateString += GetSideString(front);
        stateString += GetSideString(down);
        stateString += GetSideString(left);
        stateString += GetSideString(back);
        return stateString;
    }
}

