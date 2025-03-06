using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Grapple : MonoBehaviour
{
    public WorldLogic _worldlogic;
    public GameObject Player;
    public GameObject _playerXR;
    public Rigidbody Prb;
    public weaponHandler _handler;
    public GameObject GrappleGun;
    public GameObject GrappleHead;
    public GameObject BarrelPos;
    public GrappleHead _HeadS;

    private XRGrabInteractable _grabinteractable;

    public InputActionReference _Trig;
    public InputActionReference _Reel;
    public InputActionReference _Release;


    public float _springLen = 10f;
    public float _GrappleHeadSpeed = 20f;
    public float _GrappleHeadReelSpeed = 20f;
    public float _ReelPullSpeed = 200f;
    public float _maxswingdis = 100f;
    public float _hookgrav=10f;
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

    public LineRenderer _LineR;
    public SpringJoint _joint;

    private float _len;
    public bool _isJoint=false;

    void Start()
    {
        _worldlogic =GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
        Player = _worldlogic._Player;
        _playerXR=Player.transform.GetChild(0).gameObject;
        Prb = _playerXR.GetComponent<Rigidbody>();
        _Trig = _handler._Trig;
        _Reel = _handler._Reel;
        _Release = _handler._Release;
        //_grabinteractable = GetComponent<XRGrabInteractable>();
        //_grabinteractable.activated.AddListener(x=>GrappleShoot());
        //_grabinteractable.deactivated.AddListener(x => GrappleStop());
        //_grabinteractable.selectEntered.AddListener(OnGrab);
        //_grabinteractable.selectExited.AddListener(x=>OnRelease());

        _HeadS.HookHit.AddListener(OnHookHit);
    }

    void Update()
    {
        if (_Trig.action.triggered) GrappleShoot();
        if (_Reel.action.triggered && !_ready) _reeling = true;
        if (_Reel.action.WasReleasedThisFrame()&&_hookhit) _reeling = false;
        if (_Trig.action.WasReleasedThisFrame())
        {
            GrappleStop();
            _reeling=true;
            _released=true;
        }

        //_hookhit=_HeadS._hit;
        if (!_Shooting)
        {
            _GunDir = (GrappleGun.transform.up * -1);          
        }
        _len = Vector3.Distance(Player.transform.position, GrappleHead.transform.position);
        if (_Shooting && !_hookhit && !_reeling)
        {
            if (_len<= _maxswingdis)
            {
                GrappleHead.transform.position += ((_GunDir * _GrappleHeadSpeed)
                    + Vector3.down * Mathf.Lerp(0f, _hookgrav, 0.02f)
                    ) * Time.deltaTime;
            }
            else
            {
                GrappleHead.transform.position += (Vector3.Lerp((_GunDir * _GrappleHeadSpeed),
                     (Vector3.down * _hookgrav),0.2f)
                    ) * Time.deltaTime;
            }
        }
        if (_hookhit&&!_isJoint && _joint == null)
        {
            _joint=_playerXR.AddComponent<SpringJoint>();
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor=GrappleHead.transform.position;
          
            _joint.maxDistance = _len*1f;
            _joint.minDistance = 0f;

            _joint.spring = 4.5f;
            _joint.damper = 7f;
            _joint.massScale = 4.5f;
            _isJoint = true;
        }
        _handler._hookhit = _hookhit;
        if (_handler._resetRequest) Reset();
        if (_hookhit) this.GetComponent<gunsoundscript>().shootnow = false;
        /*
        if (!_hookhit)
        {
            SpringJoint leakjoint = _playerXR.GetComponent<SpringJoint>();
            if (leakjoint != null)
            {
                Destroy(leakjoint);
            }
        }
        */
    }

    public void FixedUpdate()
    {
        if (_reeling)
        {
            GrappleReel();
        }
    }

    public void LateUpdate()
    {
        DrawRope();
    }
    public void GrappleShoot()
    {
        if (!_ready) return;
        _Shooting = true;
        _ready = false;
        GrappleHead.transform.SetParent(null,true);
        _LineR.positionCount = 2;
        this.GetComponent<gunsoundscript>().shootnow = true;
    }
    public void GrappleStop()
    {
        //for testing 
        //_reeling = true;
        //GrappleHead.transform.SetParent(GrappleGun.transform, true);
        Destroy(_joint);
        _isJoint=false;
        this.GetComponent<gunsoundscript>().shootnow = true;
    }
    public void GrappleReel()
    {
        this.GetComponent<gunsoundscript>().shootnow = false;
        this.GetComponent<gunsoundscript>().reelnow = true;
        if (!_hookhit||_released)
        {
                _ReelDir = GrappleGun.transform.position- GrappleHead.transform.position;
                GrappleHead.transform.position += _ReelDir.normalized * _GrappleHeadReelSpeed * Time.deltaTime;
                //Hrb.AddForce (_ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime);
        }
        else if (_hookhit) 
        {
            if (_hitTag == "GrappleTo")
            {
                //pulling player
                _ReelDir = GrappleHead.transform.position - GrappleGun.transform.position;
                //move player here
                Prb.AddForce(_ReelDir.normalized *  _ReelPullSpeed* Time.deltaTime);
                float distancefrompoint=_ReelDir.magnitude;
                _joint.maxDistance = distancefrompoint;
                _joint.minDistance = 0f;
                //Prb.useGravity = false;
                //Player.transform.position += _ReelDir * Mathf.Lerp(0f, _ReelPullSpeed, 0.002f) * Time.deltaTime;
                //Prb.AddForce(_ReelDir.normalized*_ReelPullSpeed*Time.deltaTime);
                this.GetComponent<gunsoundscript>().shootnow = false;
            }
            else if (_hitTag == "GrapplePull")
            {
                //pulling obj to player
                _ReelDir = GrappleGun.transform.position - GrappleHead.transform.position;
                GrappleHead.transform.position += _ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime;
                _hitTarget.transform.SetParent(GrappleHead.transform, true);
                this.GetComponent<gunsoundscript>().shootnow = false;
            }
            
        }

        if (_ReelDir.magnitude <= 1f) Reset();
       
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
        //Prb.useGravity=true;
        if (_joint!=null) Destroy(_joint);
        _LineR.positionCount = 0;
        _isJoint = false;
        _hookhit = false;
        _ready = true;
        _Shooting = false;
        _reeling = false;
        _released = false;
        if (_hitTag == "GrapplePull") _hitTarget.transform.SetParent(null, true);
        this.GetComponent<gunsoundscript>().shootnow = false;
        this.GetComponent<gunsoundscript>().reelnow = false;
        this.GetComponent<gunsoundscript>().loadnow = true;
    }

    public void DrawRope()
    {
        if (_ready) return;
        _LineR.SetPosition(0, BarrelPos.transform.position);
        _LineR.SetPosition(1, GrappleHead.transform.position);
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
