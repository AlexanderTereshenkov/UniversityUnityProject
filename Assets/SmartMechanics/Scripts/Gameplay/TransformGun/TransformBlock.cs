using UnityEngine;

public class TransformBlock : MonoBehaviour
{

    private bool _isGrabbed;
    private Transform _followTransform;

    private void Update()
    {
        if (!_isGrabbed)
            return;

        transform.position = Vector3.Slerp(transform.position, _followTransform.position, Time.deltaTime * 10);
    }

    public void GrabObject(Transform followTransform)
    {
        _isGrabbed = true;
        _followTransform = followTransform;
        gameObject.layer = 2;
    }

    public void ReleaseObject(Transform place)
    {
        transform.parent = place;
        transform.position = place.position;
        transform.rotation = place.rotation;
        _isGrabbed = false;
        _followTransform = null;
        gameObject.layer = 6;
    }

}
