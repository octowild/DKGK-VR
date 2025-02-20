using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldLogic : MonoBehaviour
{
    public string[] _Inv;

    public GameObject _Player;
    public GameObject _Rightcontroller;
    public GameObject _Leftcontroller;

    //prefabs
    public int _wepcount = 0;
    public bool _dual;
    private GameObject _priwep;
    private GameObject _secwep;
    public GameObject _Dualgun;


    public InputActionReference _RSRI;
    public InputActionReference _RSLI;
    public InputActionReference _RGrip;

    public InputActionReference _RightStick;


    void Start()
    {
        _Inv.Append("Dual Guns");


        if (_RSRI.action.triggered)
        {
            _wepcount += 1;
        }
        else if (_RSLI.action.triggered) { 
            _wepcount -= 1;
        }
        switch (_wepcount)
        {
            case 0:
                _priwep = null;
                _secwep = null;
                _dual = false;
                break;
            case 1:
                _priwep = _Dualgun;
                _secwep = _Dualgun;
                _dual = true;
                break;
            //case 2:
             //   break;
            default:
                _priwep=null;
                _secwep=null;
                _dual=false;
                break;
        }
        if (_RGrip.action.triggered)
        {
            Instantiate(_priwep,_Rightcontroller.transform.position,_Rightcontroller.transform.rotation);
            Instantiate(_secwep,_Leftcontroller.transform.position,_Leftcontroller.transform.rotation);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
