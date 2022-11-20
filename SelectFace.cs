using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    [SerializeField] private CubeState _cubeState;
    [SerializeField] private ReadCube _readCube;
    private int _layerMask = 1 << 6; // faceLayer

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !CubeState.AutoRotating)
        {
            _readCube.ReadState();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, _layerMask))
            {
                GameObject face = hit.collider.gameObject;
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    _cubeState.up,
                    _cubeState.down,
                    _cubeState.front,
                    _cubeState.back,
                    _cubeState.left,
                    _cubeState.right
                };
                foreach(List<GameObject> cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        _cubeState.PickUp(cubeSide);
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().RotateSide(cubeSide);
                    }
                }
            }
        }
    }
}
