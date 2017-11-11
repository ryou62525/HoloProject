using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.VR.WSA.Input;

public class TapSample : MonoBehaviour, IInputHandler, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        gameObject.AddComponent<Rigidbody>();
    }

    public void OnInputUp(InputEventData eventData)
    {
        Debug.LogFormat("OnInputUp\r\nSource: {0}  SourceId: {1}", eventData.InputSource, eventData.SourceId);
    }

    public void OnInputDown(InputEventData eventData)
    {
        Debug.LogFormat("OnInputDown\r\nSource: {0}  SourceId: {1}", eventData.InputSource, eventData.SourceId);
    }
}
