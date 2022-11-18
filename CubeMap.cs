using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    private CubeState _cubeState;

    public Transform up;
    public Transform down;
    public Transform front;
    public Transform back;
    public Transform left;
    public Transform right;

    public void Set()
    {
        _cubeState = FindObjectOfType<CubeState>();

        UpdateMap(_cubeState.front, front);
        UpdateMap(_cubeState.back, back);
        UpdateMap(_cubeState.up, up);
        UpdateMap(_cubeState.down, down);
        UpdateMap(_cubeState.left, left);
        UpdateMap(_cubeState.right, right);
    }

    private void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            if (face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
            }
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Color.red;
            }
            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = Color.white;
            }
            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            i++;
        }
    }
}
