using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioactiveVacumCliner : BaseAction
{

    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private Transform _currentPoint;
    private int _currentPointIndex;
    private bool _isMoving;

    public override void BeginAction()
    {
        Debug.Log("Begin action");
        _currentPointIndex = 1;
        _currentPoint = pathPoints[_currentPointIndex];
        transform.position = pathPoints[0].position;
        _isMoving = true;
    }

    void Update()
    {
        if(_isMoving)
            Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, _currentPoint.position) < 0.001f)
        {
            if (_currentPointIndex + 1 < pathPoints.Length)
            {
                _currentPointIndex++;
                _currentPoint = pathPoints[_currentPointIndex];
            }
            else
            {
                transform.position = startPoint.position;
                _isMoving = false;
                return;
            }
        }
        var lookDirection = _currentPoint.position - transform.position;
        var newDirection = Vector3.RotateTowards(transform.forward, lookDirection, rotationSpeed * Time.deltaTime, .0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
