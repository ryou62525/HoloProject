using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;

public class HandEvent : MonoBehaviour, ISourceStateHandler, IInputHandler
{
    //入力情報
    private IInputSource _currentInputSource = null;

    //入力情報のID
    private uint _id;

    //ハンドトラッキングの状態を保持する変数
    private bool _isDrag = false;

    //線の描画に使用するポインターのGameObject
    private GameObject _pointer = null;

    //テンプレートとなるポインターオブジェクトを保持する変数
    [SerializeField]
    private GameObject _pointerOrigin = null;

    private List<GameObject> _pointers = new List<GameObject>();

    private void Start()
    {
        //このオブジェクトにジェスチャの通知させるために登録する
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    private void Update()
    {
        //ドラッグしている間対象のオブジェクトが手に追従します
        if (_isDrag)
        {
            var pos = Vector3.zero;
            _currentInputSource.TryGetPointerPosition(_id, out pos);
            _pointer.transform.position = pos;
        }
    }

    /// <summary>
    /// 人差し指をおろした時に呼ばれる関数です。
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputDown(InputEventData eventData)
    {
        if (!eventData.InputSource.SupportsInputInfo
            (eventData.SourceId, SupportedInputInfo.Position))
        {
            return;
        }

        GameObject tmp = Instantiate(_pointerOrigin);
        _pointer = tmp;
        _pointers.Add(_pointer);

        _currentInputSource = eventData.InputSource;
        _id = eventData.SourceId;
        _isDrag = true;
        Debug.Log("Is Down");
    }

    /// <summary>
    /// 人差し指を上げたときに呼ばれる関数です
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputUp(InputEventData eventData)
    {
        _isDrag = false;
        _pointer.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Is UP");
    }

    /// <summary>
    /// 手を検出したときに呼ばれる関数です。
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSourceDetected(SourceStateEventData eventData)
    {
        Debug.Log("手を検出しました");
    }

    /// <summary>
    /// 手をロストした時に呼ばれる関数です。
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSourceLost(SourceStateEventData eventData)
    {
        _isDrag = false;
        Debug.Log("手をロストしました");
    }

}
