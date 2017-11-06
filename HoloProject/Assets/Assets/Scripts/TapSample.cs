
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TapSample : MonoBehaviour, IInputClickHandler
{
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        gameObject.AddComponent<Rigidbody>();
    }
}
