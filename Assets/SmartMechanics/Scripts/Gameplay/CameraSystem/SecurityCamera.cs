using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private float cameraFOV;
    [SerializeField] private float maxDistance;
    [SerializeField] private BaseAction action;
    [SerializeField] private float reactionTime;
    [Header("Light settings")]
    [SerializeField] private Light pointLight;
    [SerializeField] private Color detectionColor;

    private Player _player;
    private float _timer;
    private Color _defaultColor;

    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        _timer = reactionTime;
        _defaultColor = pointLight.color;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (CheckPlayerIsInView())
        {
            if(_timer >= reactionTime)
            {
                BeginAction();
                _timer = 0;
            }
            pointLight.color = detectionColor;
            return;
        }
        pointLight.color = _defaultColor;
        Debug.DrawRay(transform.position, ((_player.transform.position - transform.position).normalized) * maxDistance, Color.red);
    }

    private bool CheckPlayerIsInView()
    {
        var playerVector = (_player.transform.position - transform.position);
        bool checkAngle = Vector3.Angle(transform.forward, playerVector) <= cameraFOV / 2f;
        bool checkDistance = Vector3.Distance(_player.transform.position, transform.position) <= maxDistance;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, playerVector, out hit))
        {
            Debug.Log(hit.collider.name);
            /*
            if(!hit.collider.TryGetComponent(out Player _))
            {
                return false;
            }
            */
        }
        Debug.Log("PLayer detected");
        return checkAngle && checkDistance;
    }

    private void BeginAction()
    {
        action.BeginAction();
    }
}
