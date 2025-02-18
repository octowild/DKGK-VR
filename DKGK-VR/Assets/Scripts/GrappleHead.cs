using UnityEngine;

public class GrappleHead : MonoBehaviour
{

    public bool _hit = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        _hit = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        _hit = false;
    }
}
