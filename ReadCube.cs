using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    [SerializeField] private Transform _tUp;
    [SerializeField] private Transform _tDown;
    [SerializeField] private Transform _tFront;
    [SerializeField] private Transform _tBack;
    [SerializeField] private Transform _tLeft;
    [SerializeField] private Transform _tRight;

    [SerializeField] private GameObject emptyGO;
    //

    private List<GameObject> _frontRays = new List<GameObject>();
    private List<GameObject> _backRays = new List<GameObject>();
    private List<GameObject> _upRays = new List<GameObject>();
    private List<GameObject> _downRays = new List<GameObject>();
    private List<GameObject> _leftRays = new List<GameObject>();
    private List<GameObject> _rightRays = new List<GameObject>();

    private int _layerMask = 1 << 6;

    [SerializeField] private CubeState _cubeState;
    [SerializeField] private CubeMap _cubeMap;

    private List<GameObject> facesHit = new List<GameObject>();

    private void Start()
    {
        SetRayTransform();

        _cubeState = FindObjectOfType<CubeState>();
        ReadState();
        CubeState.Started = true;
    }


    public void ReadState()
    {
        _cubeState = FindObjectOfType<CubeState>();
        _cubeMap = FindObjectOfType<CubeMap>();

        _cubeState.up = ReadFace(_upRays, _tUp);
        _cubeState.down = ReadFace(_downRays, _tDown);
        _cubeState.front = ReadFace(_frontRays, _tFront);
        _cubeState.back = ReadFace(_backRays, _tBack);
        _cubeState.left = ReadFace(_leftRays, _tLeft);
        _cubeState.right = ReadFace(_rightRays, _tRight);

        _cubeMap.Set();
    }

    private void SetRayTransform()
    {
        _upRays = BuildRays(_tUp, new Vector3(90, 90, 0));
        _downRays = BuildRays(_tDown, new Vector3(270, 90, 0));
        _leftRays = BuildRays(_tLeft, new Vector3(0, 180, 0));
        _rightRays = BuildRays(_tRight, new Vector3(0, 0, 0));
        _frontRays = BuildRays(_tFront, new Vector3(0, 90, 0));
        _backRays = BuildRays(_tBack, new Vector3(0, 270, 0));
    }

    private List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        // |0|1|2|
        // |3|4|5|
        // |6|7|8|

        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(
                    rayTransform.localPosition.x + x,
                    rayTransform.localPosition.y + y,
                    rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }

        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        facesHit = new List<GameObject>();

        foreach(GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            if (Physics.Raycast
                (ray, rayTransform.forward, out hit, Mathf.Infinity, _layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                //print(hit.collider.gameObject.name);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }
        return facesHit;
    }
}
