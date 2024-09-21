using UnityEngine;

public class NegativeEffect : MonoBehaviour
{

    private float _radiationValue;

    public void ChangeValue(float value)
    {
        _radiationValue += value;
        Debug.Log("Value changed :" + _radiationValue);
    }
}
