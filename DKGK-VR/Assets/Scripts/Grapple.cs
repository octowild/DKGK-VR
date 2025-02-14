using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Grapple : MonoBehaviour
{
    public GameObject GrappleGun;
    public GameObject GrappleHead;

    public float _GrappleHeadSpeed = 10f;

    private Vector3 _GunDir;
    private bool _Shooting=false;

    void Start()
    {
        XRGrabInteractable _grabinteractable = GetComponent<XRGrabInteractable>();
        _grabinteractable.activated.AddListener(x=>GrappleShoot());
        _grabinteractable.deactivated.AddListener(x=>GrappleStop());
    }

  
    void Update()
    {
        if (!_Shooting)
        {
            _GunDir = (GrappleGun.transform.up * -1);
        }
        if (_Shooting) {
            GrappleHead.transform.position += _GunDir * _GrappleHeadSpeed * Time.deltaTime;
        }
    }
    public void GrappleShoot()
    {
        _Shooting = true;
    }
    public void GrappleStop()
    {
        _Shooting = false;
    }
}
