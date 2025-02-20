using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldLogic : MonoBehaviour
{
    public string[] _Inv;

    public GameObject _Player;

    public InputActionReference _RightStick;
    void Start()
    {
        _Inv.Append("Dual Guns");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
