using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    [SerializeField] ReadCube _readCube;
    [SerializeField] CubeState _cubeState;
    private bool _doOnce = true;

    private GameState _gameState = new GameState();

    private void Update()
    {
        if (CubeState.Started && _doOnce)
        {
            Solver();
            _doOnce = false;
        }
    }

    public void Solver()
    {
        _readCube.ReadState();

        string moveString = _cubeState.GetStateString();
        print(moveString);

        // solve the cube
        string info = "";

        string solution = Search.solution(moveString, out info);

        List<string> solutionList = StringToList(solution);

        Automate.MoveList = solutionList;

        print(info);

        if (!_doOnce)
        {
            _gameState.ChangeGameState(GameStates.isSolving);
            print("Is solving now");
        }
    }

    private List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>
            (solution.Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }
}
