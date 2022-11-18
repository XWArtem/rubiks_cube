using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    private Vector2 _firstPressPos;
    private Vector2 _secondPressPos;
    private Vector2 _currentSwipe;

    private Vector3 _previousMousePos;
    private Vector3 _mouseDelta;

    [SerializeField] private float _rotationSpeed = 200f;

    [SerializeField] private GameObject target;

    private void Update()
    {
        Swipe();
        Drag();
    }

    private void Drag()
    {
        if (Input.GetMouseButton(1))
        {
            _mouseDelta = Input.mousePosition - _previousMousePos;
            _mouseDelta *= 0.2f;
            transform.rotation = Quaternion.Euler(_mouseDelta.y, -_mouseDelta.x, 0) * transform.rotation;
        }
        else
        {
            if (transform.rotation != target.transform.rotation)
            {
                var step = _rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards
                    (transform.rotation, target.transform.rotation, step);
            }
        }
        _previousMousePos = Input.mousePosition;
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(1))
        {
            _secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            _currentSwipe = new Vector2(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);
            _currentSwipe.Normalize();
            if (LeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(_currentSwipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (UpLeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (UpRightSwipe(_currentSwipe))
            {
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (DownLeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (DownRightSwipe(_currentSwipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
        }
    }

    private bool LeftSwipe(Vector2 swipe)
    {
        return _currentSwipe.x < 0f && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    }

    private bool RightSwipe(Vector2 swipe)
    {
        return _currentSwipe.x > 0f && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    }

    private bool UpLeftSwipe(Vector2 swipe)
    {
        return _currentSwipe.y > 0f && _currentSwipe.x < 0f;
    }

    private bool UpRightSwipe(Vector2 swipe)
    {
        return _currentSwipe.y > 0f && _currentSwipe.x > 0f;
    }
    private bool DownLeftSwipe(Vector2 swipe)
    {
        return _currentSwipe.y < 0f && _currentSwipe.x < 0f;
    }

    private bool DownRightSwipe(Vector2 swipe)
    {
        return _currentSwipe.y < 0f && _currentSwipe.x > 0f;
    }


}
