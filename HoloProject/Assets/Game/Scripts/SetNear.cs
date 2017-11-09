using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNear : MonoBehaviour
{
    private float _nearVal = 0.1f;
    private Camera _camera = null;

    private void Awake()
    {
        _camera = Camera.main;
        if (!_camera) Debug.Log("カメラがセットされていません");
    }

    public void OnClickedUp()
    {
        if (_camera != null)
        {
            _camera.nearClipPlane += _nearVal;
        }
    }

    public void OnClickedDown()
    {
        if (_camera != null)
        {
            _camera.nearClipPlane -= _nearVal;
        }
    }

}
