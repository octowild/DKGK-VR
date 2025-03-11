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
    public string _add=" m/s";
    public float _velo;
     

    void Start()
    {
        _worldLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
    }

    void Update()
    {
        _velo = _Player.linearVelocity.magnitude;
        _package.text= _worldLogic._packageCount.ToString()+_add;
        _speedOMeter.text = _velo.ToString();
    }
}
