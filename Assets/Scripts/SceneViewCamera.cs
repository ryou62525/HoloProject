using UnityEngine;

/// <summary>
/// Mainとなるカメラにコンポーネントとしてアタッチすることでマウスで操作できます
/// </summary>
[RequireComponent(typeof(Camera))]
public class SceneViewCamera : MonoBehaviour
{

    [SerializeField, Range(0.1f, 10f)]
    private float _wheelSpeed = 1f;

    [SerializeField, Range(0.1f, 10f)]
    private float _moveSpeed = 0.3f;

    [SerializeField, Range(0.1f, 10f)]
    private float _rotateSpeed = 0.3f;

    private Vector3 _preMousePos;

    private void Update()
    {
        MouseUpdate();
    }

    /// <summary>
    /// マウスの更新処理
    /// </summary>
    private void MouseUpdate()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (!scrollWheel.Equals(0.0f)) MouseWheel(scrollWheel);

        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
            _preMousePos = Input.mousePosition;

        MouseDrag(Input.mousePosition);
    }

    /// <summary>
    /// マウスホイールの処理
    /// </summary>
    /// <param name="delta">Delta.</param>
    private void MouseWheel(float delta)
    {
        transform.position += transform.forward * delta * _wheelSpeed;
    }

    /// <summary>
    /// マウスドラッグのしている時の処理
    /// </summary>
    /// <param name="mousePos">Mouse position.</param>
    private void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - _preMousePos;

        if (diff.magnitude < Vector3.kEpsilon)
            return;

        if (Input.GetMouseButton(2))
            transform.Translate(-diff * Time.deltaTime * _moveSpeed);
        else if (Input.GetMouseButton(1))
            CameraRotate(new Vector2(-diff.y, diff.x) * _rotateSpeed);

        _preMousePos = mousePos;
    }

    /// <summary>
    /// カメラの回転の計算をします。
    /// </summary>
    /// <param name="angle">Angle.</param>
    public void CameraRotate(Vector2 angle)
    {
        transform.RotateAround(transform.position, transform.right, angle.x);
        transform.RotateAround(transform.position, Vector3.up, angle.y);
    }
}