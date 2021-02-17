using UnityEngine;

namespace NatureVR.Utility
{
    /// <summary>
    /// カメラをマウスで操作する機能を提供します。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class CameraController : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private Camera m_Camera = null;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField, Range(0, 50)]
        private float m_MoveSpeed = 25;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField, Range(0, 100)]
        private float m_ZoomSpeed = 50;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField, Range(0, 360)]
        private float m_RotateSpeed = 180;

        /// <summary>
        /// 毎フレーム呼び出されます。
        /// </summary>
        private void Update()
        {
            // マウスの入力を取得
            var mouseAxis = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
            var mouseRight = UnityEngine.Input.GetMouseButton(1);
            var mouseMiddle = UnityEngine.Input.GetMouseButton(2);
            var mouseScroll = UnityEngine.Input.mouseScrollDelta.y;

            // 入力に応じてカメラの座標を更新
            if (mouseRight)
            {
                RotateCamera(mouseAxis);
            }
            if (mouseMiddle)
            {
                MoveCamera(mouseAxis);
            }
            ZoomCamera(mouseScroll);
        }

        /// <summary>
        /// カメラを平行に移動します。
        /// </summary>
        private void MoveCamera(Vector2 axis)
        {
            var translation = axis * m_MoveSpeed * Time.deltaTime;
            m_Camera.transform.Translate(new Vector3(-translation.x, -translation.y, 0), Space.Self);
        }

        /// <summary>
        /// カメラを前後に移動します。
        /// </summary>
        private void ZoomCamera(float axis)
        {
            var translation = axis * m_ZoomSpeed * Time.deltaTime;
            m_Camera.transform.Translate(new Vector3(0, 0, translation), Space.Self);
        }

        /// <summary>
        /// カメラを回転します。
        /// </summary>
        private void RotateCamera(Vector2 axis)
        {
            var angle = axis * m_RotateSpeed * Time.deltaTime;
            m_Camera.transform.RotateAround(m_Camera.transform.position, Vector3.up, angle.x);
            m_Camera.transform.RotateAround(m_Camera.transform.position, transform.right, -angle.y);
        }
    }
}