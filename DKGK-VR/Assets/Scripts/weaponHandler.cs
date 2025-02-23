using UnityEngine;
using UnityEngine.InputSystem;

public class weaponHandler : MonoBehaviour
{
    public GameObject _wep;

    public bool _hookhit;
    public bool _resetRequest=false;
    public InputActionReference _Trig;
    public InputActionReference _Reel;
    public InputActionReference _Release;
    void Start()
    {

    }


    void Update()
    {
        
    }

    public void Reset()
    {
        _resetRequest = true;
    }
}
