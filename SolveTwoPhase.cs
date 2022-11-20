using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    [SerializeField] ReadCube _readCube;
    [SerializeField] CubeState _cubeState;
    private bool _doOnce = true;

    private void Update()
    {
        if (CubeState.Started && _doOnce)
        {
            _doOnce = false;
            Solver();
        }
    }

    public void Solver()
    {
        _readCube.ReadState();

        string moveString = _cubeState.GetStateString();
        print(moveString);

        // solve the cube
        string info = "";

        // first table building
        //string solution = SearchRunTime.solution(moveString, out info, buildTables: true);

        // otherwise
        string solution = Search.solution(moveString, out info);

        List<string> solutionList = StringToList(solution);

        Automate.MoveList = solutionList;

        print(info);
    }

    private List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>
            (solution.Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }


}
