using UnityEngine;

public class BeaconArea : MonoBehaviour
{
    public bool _trigger = false;

    public void OnTriggerEnter(Collider other)
    {
        _trigger = true;
    }
}
