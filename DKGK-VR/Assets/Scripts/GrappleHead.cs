using UnityEngine;
using UnityEngine.Events;

public class GrappleHead : MonoBehaviour
{
    public UnityEvent<bool> HookHit;
    public bool _hit = false;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(_hit);
    }

    private void OnTriggerEnter(Collider other)
    {
        HookHit.Invoke(true);
        _hit = true;
    }
    private void OnTriggerExit(Collider other)
    {
        HookHit.Invoke(false);
        _hit = false;
    }

}
