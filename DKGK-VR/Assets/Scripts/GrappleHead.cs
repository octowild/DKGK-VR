using UnityEngine;

public class GrappleHead : MonoBehaviour
{

    public bool _hit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _hit = true;
    }
}
