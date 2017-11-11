using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PolygonCounter : MonoBehaviour
{
    [SerializeField]
    private int minFps = 60;

    [SerializeField]
    private Text[] _texts = null;

    [SerializeField]
    private string[] _headers = null;

    private int _vertices;
    private int _polygons;
    private int _objects;

	private int _frameCount;
	private float _elapsedTime;
	private double _frameRate;

    private float nextTime = 0.0f;

    private enum DebugVal
    {
		FPS,
		VERTICES,
        POLYGONS,
        OBJECTS,
    }

    private void Awake()
    {
        _vertices = 0;
        _polygons = 0;
        _objects = 0;
    }

    private void Start()
    {
        nextTime = Time.time + 1;
        StartCoroutine(DispState());
    }

    /// <summary>
    /// 各ステータスをコルーチンを使用ディスプレイに表示
    /// </summary>
    /// <returns>The state.</returns>
    private IEnumerator DispState()
    {
        while (true)
        {
            _frameCount++;
            _elapsedTime += Time.deltaTime;
            // 1秒ごとにFPS検証
            if (_elapsedTime >= 0.5f)
            {
				_frameRate = System.Math.Round(_frameCount / _elapsedTime, 1, System.MidpointRounding.AwayFromZero);
				_frameCount = 0;
				_elapsedTime = 0;

                if (_frameCount < minFps) PolygonCount(_frameRate);
                _frameCount = 0;
            }
            yield return null;
        }
    }

	/// <summary>
	/// ポリゴン数、頂点数を算出します。
	/// </summary>
	/// <param name="frameRate">Frame rate.</param>
	private void PolygonCount(double frameRate)
    {
        _vertices = 0;
        _polygons = 0;
		_objects = 0;

        //シーン上にある全てのオブジェクトを取得
        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
        {
            //オブジェクトがなければ計算せず終了
            if (!obj.activeInHierarchy) return;

            if(obj.GetComponent<MeshRenderer>() != null)
            {
                _objects += 1;
            }

            SkinnedMeshRenderer skin = obj.GetComponent<SkinnedMeshRenderer>();
            if (skin != null)
            {
                int vert = skin.sharedMesh.vertices.Length;
                _vertices += vert;

                int polygon = skin.sharedMesh.triangles.Length / 3;
                _polygons += polygon;
            }

            MeshFilter mesh = obj.GetComponent<MeshFilter>();

            if (mesh != null)
            {
                int vert = mesh.sharedMesh.vertices.Length;
                _vertices += vert;

                int polygon = mesh.sharedMesh.triangles.Length / 3;
                _polygons += polygon;
            }
        }

        SetDebugValueToText( _vertices, _polygons, _objects, frameRate);

        //コンソール表示
        //Debug.LogFormat("Vertices(verts) : {0} , Polygons(Tris) : {1} , FPS : {2} ",
            //_vertices, _polygons, _debugValues[(int)DebugState.FPS]);
    }

    /// <summary>
    /// UI.Textにデバッグの値を配置します
    /// </summary>
    private void SetDebugValueToText(int vertices, int polygons, int objects, double fps)
    {
        _texts[(int)DebugVal.FPS].text = _headers[(int)DebugVal.FPS] + fps;
        _texts[(int)DebugVal.VERTICES].text = _headers[(int)DebugVal.VERTICES] + vertices;
        _texts[(int)DebugVal.POLYGONS].text = _headers[(int)DebugVal.POLYGONS] + polygons;
        _texts[(int)DebugVal.OBJECTS].text = _headers[(int)DebugVal.OBJECTS] + objects;
    }
}



