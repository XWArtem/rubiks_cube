using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Automate : MonoBehaviour
{
    public static List<string> MoveList = new List<string>() { };
    private readonly List<string> allMoves = new List<string>()
    {
        "U","D","F","B","L","R",
        "U2","D2","F2","B2","L2","R2",
        "U'","D'","F'","B'","L'","R'",
    };

    [SerializeField] private CubeState _cubeState;
    [SerializeField] private ReadCube _readCube;

    private void Update()
    {
        if (MoveList.Count > 0 && !CubeState.AutoRotating)
        {
            PerfomMove(MoveList[0]);

            MoveList.Remove(MoveList[0]);
        }
    }

    public void Shuffle()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(10, 30);
        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
        }
        MoveList = moves;
        //CubeState.IsSolving = true;
    }

    private void PerfomMove(string move)
    {
        _readCube.ReadState();
        CubeState.AutoRotating = true;
        if (move == "U")
        {
            RotateSide(_cubeState.up, -90);
        }
        if (move == "U'")
        {
            RotateSide(_cubeState.up, 90);
        }
        if (move == "U2")
        {
            RotateSide(_cubeState.up, -180);
        }
        if (move == "B")
        {
            RotateSide(_cubeState.back, -90);
        }
        if (move == "B'")
        {
            RotateSide(_cubeState.back, 90);
        }
        if (move == "B2")
        {
            RotateSide(_cubeState.back, -180);
        }
        if (move == "D")
        {
            RotateSide(_cubeState.down, -90);
        }
        if (move == "D'")
        {
            RotateSide(_cubeState.down, 90);
        }
        if (move == "D2")
        {
            RotateSide(_cubeState.down, -180);
        }
        if (move == "L")
        {
            RotateSide(_cubeState.left, -90);
        }
        if (move == "L'")
        {
            RotateSide(_cubeState.left, 90);
        }
        if (move == "L2")
        {
            RotateSide(_cubeState.left, -180);
        }
        if (move == "R")
        {
            RotateSide(_cubeState.right, -90);
        }
        if (move == "R'")
        {
            RotateSide(_cubeState.right, 90);
        }
        if (move == "R2")
        {
            RotateSide(_cubeState.right, -180);
        }
        if (move == "F")
        {
            RotateSide(_cubeState.front, -90);
        }
        if (move == "F'")
        {
            RotateSide(_cubeState.front, 90);
        }
        if (move == "F2")
        {
            RotateSide(_cubeState.front, -180);
        }
    }

    private void RotateSide(List<GameObject> side, float angle)
    {
        PivotRotation pivotRotation = side[4].transform.parent.GetComponent<PivotRotation>();
        pivotRotation.StartAutoRotate(side, angle);
    }
}
