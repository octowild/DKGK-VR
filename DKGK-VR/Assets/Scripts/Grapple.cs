using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Grapple : MonoBehaviour
{
    public WorldLogic _worldlogic;
    public GameObject Player;
    public Rigidbody Prb;
    public GameObject GrappleGun;
    public GameObject GrappleHead;
    public GameObject BarrelPos;
    public GrappleHead _HeadS;

    private XRGrabInteractable _grabinteractable;

    public InputActionReference _Trig;
    public InputActionReference _Reel;
    public InputActionReference _Release;



    public float _GrappleHeadSpeed = 20f;
    public float _ReelPullSpeed = 50f;
   // private InputActionReference _RH;

    private Vector3 _GunDir;
    private Vector3 _ReelDir;
    public bool _Shooting=false;
    public bool _ready = true;
    public bool _reeling = false;
    public bool _hookhit = false;
    public bool _released = false;
    public string _hitTag;
    public GameObject _hitTarget;

    void Start()
    {
        _worldlogic =GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
        Player = _worldlogic._Player;
        Prb = Player.transform.GetChild(0).GetComponent<Rigidbody>();
        //_grabinteractable = GetComponent<XRGrabInteractable>();
        //_grabinteractable.activated.AddListener(x=>GrappleShoot());
        //_grabinteractable.deactivated.AddListener(x => GrappleStop());
        //_grabinteractable.selectEntered.AddListener(OnGrab);
        //_grabinteractable.selectExited.AddListener(x=>OnRelease());

        _HeadS.HookHit.AddListener(OnHookHit);
    }

  // add button for reeling
  //move player
    void Update()
    {
        if (_Trig.action.triggered) GrappleShoot();
        if (_Reel.action.triggered && !_ready) _reeling = true;
        if (_Release.action.triggered)
        {
            _reeling=true;
            _released=true;
        }
        //_hookhit=_HeadS._hit;
        if (!_Shooting)
        {
            _GunDir = (GrappleGun.transform.up * -1);          
        }
        if (_Shooting && !_hookhit  &&!_reeling)
        {
            GrappleHead.transform.position += _GunDir * _GrappleHeadSpeed * Time.deltaTime;
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
    //public void GrappleStop()
    //{
        //for testing 
        //_reeling = true;
        //GrappleHead.transform.SetParent(GrappleGun.transform, true);

    //}
    public void GrappleReel()
    {
        if (!_hookhit||_released)
        {
                _ReelDir = GrappleGun.transform.position- GrappleHead.transform.position;
                GrappleHead.transform.position += _ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime;
                //Hrb.AddForce (_ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime);
        }
        else if (_hookhit) 
        {
            if (_hitTag == "GrappleTo")
            {
                //pulling player
                _ReelDir = GrappleHead.transform.position - GrappleGun.transform.position;
                //move player here
                Player.transform.position += _ReelDir * Mathf.Lerp(0f, _ReelPullSpeed, 0.02f) * Time.deltaTime;
                //Prb.AddForce(_ReelDir.normalized*_ReelPullSpeed*Time.deltaTime);
            }else if (_hitTag == "GrapplePull")
            {
                //pulling obj to player
                _ReelDir = GrappleGun.transform.position - GrappleHead.transform.position;
                GrappleHead.transform.position += _ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime;
                _hitTarget.transform.SetParent(GrappleHead.transform, true);
            }
            
        }

        if (_ReelDir.magnitude <= 2f)
        {
            GrappleHead.transform.position = BarrelPos.transform.position;
            GrappleHead.transform.localRotation = Quaternion.Euler(0f,0f,0f); 
            GrappleHead.transform.SetParent(GrappleGun.transform, true);
            _hookhit = false;
            _ready = true;
            _Shooting = false;
            _reeling = false;
            _released = false;
            if (_hitTag == "GrapplePull")
            {
                _hitTarget.transform.SetParent(null, true);
            }
        }
        
    }
    public void OnHookHit(bool hookhit, GameObject hittarget)
    {
        _hookhit = hookhit;
        _hitTarget = hittarget;
        _hitTag = hittarget.transform.tag;

    }
    public void Reset()
    {
        GrappleHead.transform.position = BarrelPos.transform.position;
        GrappleHead.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        GrappleHead.transform.SetParent(GrappleGun.transform, true);
    }
    /*   private void OnGrab(SelectEnterEventArgs args)
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
           _hookhit = false;
           _ready = true;
           _Shooting = false;
           _reeling = false;
           GrappleHead.transform.position = BarrelPos.transform.position;
           GrappleHead.transform.SetParent(GrappleGun.transform, true);
           //_RH = null;
       }
    */
}
