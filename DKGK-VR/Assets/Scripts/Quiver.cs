using UnityEngine;
using UnityEngine.InputSystem;

public class Quiver : MonoBehaviour
{

    public weaponHandler _handler;
    public GameObject _bow;
    public WorldLogic _worldlogic;
    public GameObject _drawnarrow;
    public Transform _attach;
    public GameObject _arrowprefab;

    public InputActionReference _trig;
    public bool _drawarrow=false;
    public bool _ready = true;
    void Start()
    {
        _worldlogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
        _bow=_worldlogic._currsecwep;
        _trig = _handler._Trig;
    }


    void Update()
    {
        if (_trig.action.triggered&&_ready)
        {
            _drawarrow = true;
        }
        //if (_drawarrow) DrawArrow();
    }

    public void DrawArrow()
    {
        _drawnarrow=Instantiate(_arrowprefab,_attach);
    }

    public void RotateArrow(Transform _start,Transform _end)
    {

    }
}
