using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;

public class ActivateManual : MonoBehaviour
{

    [SerializeField]
    private ImmediatePositionWithLocationProvider ipwlProvider;
    [SerializeField]
    private RotateWithLocationProvider rwlProvider;

    // Use this for initialization
    void Start()
    {
        ipwlProvider.enabled = false;
        rwlProvider.enabled = false;
    }
    public void onToggleProviders()
    {
        ipwlProvider.enabled = !ipwlProvider.enabled;
        rwlProvider.enabled = !rwlProvider.enabled;
        if (!ipwlProvider.enabled)
        {
            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
