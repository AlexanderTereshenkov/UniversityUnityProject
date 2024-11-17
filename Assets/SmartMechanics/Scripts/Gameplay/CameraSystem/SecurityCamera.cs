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
    private float _timer;
    private float _lastReactionTime;
    private Color _defaultColor;

    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        _timer = actionTime;
        _defaultColor = pointLight.color;
        _lastReactionTime = Time.time;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (CheckPlayerIsInView())
        {
            if(_timer >= actionTime)
            {
                BeginAction();
                _timer = 0;
            }
            _lastReactionTime = Time.time;
            pointLight.color = detectionColor;
            return;
        }
        else
        {
            _lastReactionTime = Time.time;
        }
        pointLight.color = _defaultColor;
        Debug.DrawRay(transform.position, ((_player.transform.position - transform.position).normalized) * maxDistance, Color.red);
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
