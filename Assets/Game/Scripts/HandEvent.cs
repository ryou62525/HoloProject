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

    //線の描画をするGameObject
    private GameObject _lineObject = null;

    //_lineObjectを保存するList配列
    private List<GameObject> _lineObjects = new List<GameObject>();

    //LineRedererのコンポーネントを持ったオブジェクトを保持する変数
    [SerializeField]
    private GameObject _OriginObj = null;


    private void Start()
    {
        //ジェスチャ通知をこのオブジェクトに登録
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    private void Update()
    {
        //ドラッグしている間対象のオブジェクトが手に追従します
        if (_isDrag)
        {
            var pos = Vector3.zero;
            _currentInputSource.TryGetPointerPosition(_id, out pos);
            _lineObject.transform.position = pos;
        }
    }

    /// <summary>
    /// 人差し指をおろした時に呼ばれる関数です。
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputDown(InputEventData eventData)
    {
        //手の位置を取得できるか確認する取得できない場合は終了
        if (!eventData.InputSource.SupportsInputInfo
            (eventData.SourceId, SupportedInputInfo.Position)) return;
       
        
        var tmp = Instantiate(_OriginObj);
        _lineObject = tmp;
        _lineObjects.Add(_lineObject);

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
        Initialize();
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
        Initialize();
        Debug.Log("手をロストしました");
    }

    /// <summary>
    /// 人差し指を上げた時と手をロストしたときに、
    /// TraileRendererのコンポーネントを持ったオブジェクトの
    /// パラメータなどを初期化する関数です。
    /// </summary>
    private void Initialize()
    {
        _isDrag = false;
        _lineObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
