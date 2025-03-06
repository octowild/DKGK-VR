using UnityEngine;
using UnityEngine.Events;

public class GrappleHead : MonoBehaviour
{
    public UnityEvent<bool,GameObject> HookHit;
    //public bool _hit = false;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6|| other.gameObject.layer != 8)
        {
            HookHit.Invoke(true, other.gameObject);
        }
        //_hit = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 6 || other.gameObject.layer != 8)
        {
            HookHit.Invoke(false, other.gameObject);
        }
        //_hit = false;
    }

}
