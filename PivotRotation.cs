using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> _activeSide;
    private Vector3 _localForward;
    private Vector3 _mouseRef;
    private bool _isDragging;
    private bool _autoRotating;

    private float _dragDistance = 0.4f;
    private Vector3 _rotation;
    private float _rotationSpeed = 300f;

    private Quaternion _targetQuaternion;

    private ReadCube _readCube;
    private CubeState _cubeState;

    private void Start()
    {
        _readCube = FindObjectOfType<ReadCube>();
        _cubeState = FindObjectOfType<CubeState>();
    }

    private void LateUpdate()
    {
        if (_isDragging && !_autoRotating)
        {
            SpinSide(_activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                RotateToRightAngle();
            }
        }
        if (_autoRotating)
        {
            AutoRotate();
        }
    }

    private void SpinSide(List<GameObject> side)
    {
        _rotation = Vector3.zero;
        Vector3 mouseOffset = (Input.mousePosition - _mouseRef);
        
        if(side == _cubeState.front)
        {
            _rotation.x = (mouseOffset.x - mouseOffset.y) * _dragDistance * -1;
        }
        if (side == _cubeState.up)
        {
            _rotation.y = (mouseOffset.x + mouseOffset.y) * _dragDistance * -1;
        }
        if (side == _cubeState.down)
        {
            _rotation.y = (mouseOffset.x + mouseOffset.y) * _dragDistance * -1;
        }
        if (side == _cubeState.back)
        {
            _rotation.x = (mouseOffset.x + mouseOffset.y) * _dragDistance * 1;
        }
        if (side == _cubeState.left)
        {
            _rotation.z = (mouseOffset.x - mouseOffset.y) * _dragDistance * 1;
        }
        if (side == _cubeState.right)
        {
            _rotation.z = (mouseOffset.x + mouseOffset.y) * _dragDistance * -1;
        }

        transform.Rotate(_rotation, Space.Self);

        _mouseRef = Input.mousePosition;
    }

    public void RotateSide(List<GameObject> side)
    {
        _activeSide = side;
        _mouseRef = Input.mousePosition;
        _isDragging = true;
        _localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        _cubeState.PickUp(side);
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        _targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        _activeSide = side;
        _autoRotating = true;
    }

    public void RotateToRightAngle()
    {
        Vector3 targetAngle = transform.localEulerAngles;
        targetAngle.x = Mathf.Round(targetAngle.x / 90) * 90;
        targetAngle.y = Mathf.Round(targetAngle.y / 90) * 90;
        targetAngle.z = Mathf.Round(targetAngle.z / 90) * 90;

        _targetQuaternion.eulerAngles = targetAngle;
        _autoRotating = true;
    }

    private void AutoRotate()
    {
        _isDragging = false;
        var step = _rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards
            (transform.localRotation, _targetQuaternion, step);

        if (Quaternion.Angle(transform.localRotation, _targetQuaternion) <= 1)
        {
            transform.localRotation = _targetQuaternion;
            _cubeState.PutDown(_activeSide, transform.parent); 
            _readCube.ReadState();
            CubeState.AutoRotating = false;
            _autoRotating = false;
            _isDragging = false;
        }
    }
}
