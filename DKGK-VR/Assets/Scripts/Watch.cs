using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Watch : MonoBehaviour
{
    public WorldLogic _worldLogic;
    public Rigidbody _Player;
    public TextMeshProUGUI _package;
    public TextMeshProUGUI _speedOMeter;
    public string _add=" km/hr";
    public float _velo;
     

    void Start()
    {
        _worldLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
    }

    void Update()
    {
        _velo = _Player.linearVelocity.magnitude*18/5;
        _package.text= _worldLogic._packageCount.ToString();
        _speedOMeter.text = _velo.ToString("0.00") + _add;
    }
}
