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
     

    void Start()
    {
        _worldLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
    }

    void Update()
    {
        _package.text= _worldLogic._packageCount.ToString();
        _speedOMeter.text = _Player.maxAngularVelocity.ToString();
    }
}
