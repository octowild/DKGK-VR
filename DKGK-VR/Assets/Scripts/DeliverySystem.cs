using UnityEngine;

public class DeliverySystem : MonoBehaviour
{

    public bool _outForDelivery=false;
    public bool _BeaconSpawned = false;
    public bool _IsPickup = true;

    public GameObject _CurrentBeacon;

    public GameObject _DeliveryPickupBeaconPrefab;
    public GameObject _DeliveryDropoffBeaconPrefab;


    void Start()
    {
        
    }



    void Update()
    {
        if (!_BeaconSpawned)
        {

        }
    }
}
