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
    public float _ReelPullSpeed = 2f;

    private Vector3 _GunDir;
    private Vector3 _ReelDir;
    private bool _Shooting=false;
    private bool _HeadCol=false;
    private bool _Reeling=false;

    void Start()
    {
        XRGrabInteractable _grabinteractable = GetComponent<XRGrabInteractable>();
        _grabinteractable.activated.AddListener(x=>GrappleShoot());
        _grabinteractable.deactivated.AddListener(x=>GrappleStop());
    }

  // add button for reeling
  //move player
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
        if (_Reeling||!_Shooting) {
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
        //GrappleHead.transform.position=BarrelPos.transform.position;
    }
    public void GrappleReel()
    {
        if (!_HeadCol||!_Shooting)
        {
                _ReelDir = BarrelPos.transform.position- GrappleHead.transform.position;
                GrappleHead.transform.position += _ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime;
        }
        else if (_HeadCol) 
        {
            _ReelDir = GrappleHead.transform.position- BarrelPos.transform.position;
            //move player here
            //_ .transform.position+=_ReelDir*_ReelPullSpeed*Time.deltaTime;
        }

        if (_ReelDir.magnitude <= 5f)
        {
            GrappleHead.transform.position = BarrelPos.transform.position;
            _Reeling = false;
        }
        
    }
}
