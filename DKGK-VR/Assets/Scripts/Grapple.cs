using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Grapple : MonoBehaviour
{
    public GameObject Player;
    //public Rigidbody Prb;
    public GameObject GrappleGun;
    public GameObject GrappleHead;
    public GameObject BarrelPos;
    public GrappleHead _HeadS;

    private XRGrabInteractable _grabinteractable;

    public InputActionReference _ReelR;
    public InputActionReference _ReelL;

    public float _GrappleHeadSpeed = 10f;
    public float _ReelPullSpeed = 2f;
    private InputActionReference _RH;

    private Vector3 _GunDir;
    private Vector3 _ReelDir;
    public bool _Shooting=false;
    public bool _ready = true;
    public bool _reeling = false;
    public bool _hookhit = false;

    void Start()
    {
        _grabinteractable = GetComponent<XRGrabInteractable>();
        _grabinteractable.activated.AddListener(x=>GrappleShoot());
        _grabinteractable.deactivated.AddListener(x => GrappleStop());
        _grabinteractable.selectEntered.AddListener(OnGrab);
        _grabinteractable.selectExited.AddListener(x=>OnRelease());
       // Prb.freezeRotation = true;

        _HeadS.HookHit.AddListener(OnHookHit);
        _RH = _ReelR;
    }

  // add button for reeling
  //move player
    void Update()
    {
        //_hookhit=_HeadS._hit;
        if (!_Shooting)
        {
            _GunDir = (GrappleGun.transform.up * -1);          
        }

        if (_Shooting && !_hookhit  &&!_reeling)
        {
            GrappleHead.transform.position += _GunDir * _GrappleHeadSpeed * Time.deltaTime;
            //Hrb.AddForce(_GunDir * _GrappleHeadSpeed * Time.deltaTime);
        }
        //get which hand its on and get respective button
        //if (_grabinteractable.holdingHand == XRHand.Right){ }
        if (_RH.action.triggered && !_ready) {
            _reeling=true;
        }
        if (_reeling) {
            GrappleReel();
        }


    }
    public void GrappleShoot()
    {
        if (_ready) {
            _Shooting = true;
            _ready = false;
            GrappleHead.transform.SetParent(null,true);
        }
    }
    public void GrappleStop()
    {
        //for testing 
        _reeling = true;
        //GrappleHead.transform.SetParent(GrappleGun.transform, true);

    }
    public void GrappleReel()
    {
        if (!_hookhit)
        {
                _ReelDir = GrappleGun.transform.position- GrappleHead.transform.position;
                GrappleHead.transform.position += _ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime;
                //Hrb.AddForce (_ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime);
        }
        else if (_hookhit) 
        {
            _ReelDir = GrappleHead.transform.position- GrappleGun.transform.position;
            //move player here
            Player.transform.position+=_ReelDir*Mathf.Lerp(0f,_ReelPullSpeed,0.02f)*Time.deltaTime;
            //Prb.AddForce(_ReelDir.normalized*_ReelPullSpeed*Time.deltaTime);
            
        }

        if (_ReelDir.magnitude <= 2f)
        {
            GrappleHead.transform.position = BarrelPos.transform.position;
            GrappleHead.transform.rotation = Quaternion.Euler(1.5714f,0f,0f); 
            GrappleHead.transform.SetParent(GrappleGun.transform, true);
            _hookhit = false;
            _ready = true;
            _Shooting = false;
            _reeling = false;
        }
        
    }
    public void OnHookHit(bool hookhit)
    {
        _hookhit = hookhit;
    }
    private void OnGrab(SelectEnterEventArgs args)
    {

        if (args.interactorObject.transform.tag=="LeftHand")
        {
            _RH = _ReelL;
        }
        else if (args.interactorObject.transform.tag == "RightHand")
        {
            _RH = _ReelR;
        }
    }
    public void OnRelease()
    {
        //_RH = null;
    }
}
