using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private float cameraFOV;
    [SerializeField] private float maxDistance;
    [SerializeField] private BaseAction action;
    [SerializeField] private float actionTime;
    [SerializeField] private float reactionTime;
    [Header("Light settings")]
    [SerializeField] private Light pointLight;
    [SerializeField] private Color detectionColor;

    private Player _player;
    private float _actionTimer;
    private float _reactionTimer;
    private Color _defaultColor;

    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        _actionTimer = actionTime;
        _defaultColor = pointLight.color;
    }

    private void Update()
    {
        _actionTimer += Time.deltaTime;

        if (CheckPlayerIsInView())
        {
            if(reactionTime >= _reactionTimer)
            {
                _reactionTimer += Time.deltaTime;
                return;
            }
            if(_actionTimer >= actionTime)
            {
                BeginAction();
                _actionTimer = 0;
            }
            if(pointLight.color != detectionColor)
            {
                pointLight.color = detectionColor;
            }
            return;
        }
        else
        { 
            _reactionTimer = 0;
        }

        if(pointLight.color != _defaultColor)
        {
            pointLight.color = _defaultColor;
        }

    }

    private bool CheckPlayerIsInView()
    {
        var playerVector = _player.transform.position - transform.position;
        bool checkAngle = Vector3.Angle(transform.forward, playerVector) <= cameraFOV / 2f;
        bool checkDistance = Vector3.Distance(_player.transform.position, transform.position) <= maxDistance;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, playerVector, out hit))
        {
            if(!hit.collider.TryGetComponent(out Player _))
            {
                return false;
            }
        }
        return checkAngle && checkDistance;
    }

    private void BeginAction()
    {
        action.BeginAction();
    }
}
