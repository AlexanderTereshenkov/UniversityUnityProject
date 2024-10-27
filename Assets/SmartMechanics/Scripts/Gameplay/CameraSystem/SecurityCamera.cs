using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private float cameraFOV;
    [SerializeField] private float maxDistance;
    [SerializeField] private BaseAction action;
    [SerializeField] private float reactionTime;

    private Player _player;
    private float _timer;

    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        _timer = reactionTime;
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
        }
    }

    private bool CheckPlayerIsInView()
    {
        var playerVector = _player.transform.position - transform.position;
        bool checkAngle = Vector3.Angle(transform.forward, playerVector) <= cameraFOV / 2f;
        bool checkDistance = Vector3.Distance(_player.transform.position, transform.position) <= maxDistance;
        return checkAngle && checkDistance;
    }

    private void BeginAction()
    {
        action.BeginAction();
    }
}
