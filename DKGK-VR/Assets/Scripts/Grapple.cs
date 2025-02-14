using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Grapple : MonoBehaviour
{
    public GameObject GrappleGun;
    public GameObject GrappleHead;
    public GameObject BarrelPos;

    public float _GrappleHeadSpeed = 10f;

    private Vector3 _GunDir;
    private Vector3 _ReelDir;
    private bool _Shooting=false;
    private bool _HeadCol=false;
    private bool _Attached = false;
    private bool _release=false;
    private bool _Reeling=false;

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
        if (_Shooting && !_HeadCol)
        {
            GrappleHead.transform.position += _GunDir * _GrappleHeadSpeed * Time.deltaTime;
        }
        else if (_Shooting && _HeadCol)
        {
            _Attached = true;
        }
        if (_Reeling) {
            GrappleReel();
        }
    }
    public void GrappleShoot()
    {
        _Shooting = true;
    }
    public void GrappleStop()
    {
        _Shooting = false;
        _release = true;
        //GrappleHead.transform.position=BarrelPos.transform.position;
    }
    public void GrappleReel()
    {
        if (!_HeadCol)
        {
            if (_Attached||_release)
            {
                _ReelDir = BarrelPos.transform.position- GrappleHead.transform.position;
                GrappleHead.transform.position += _ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime;
            }
            else
            {
                _ReelDir = GrappleHead.transform.position- BarrelPos.transform.position;
                //move player here
            }



            if (_ReelDir.magnitude <= 5f)
            {
                GrappleHead.transform.position=BarrelPos.transform.position;
                _Reeling=false;
            }
        }
    }
}
