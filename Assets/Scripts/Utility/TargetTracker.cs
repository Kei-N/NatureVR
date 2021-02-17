using UnityEngine;

namespace NatureVR.Utility
{
    /// <summary>
    /// 指定した対象の座標を追跡する機能を提供します。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TargetTracker : MonoBehaviour
    {
        /// <summary>
        /// 追跡する対象
        /// </summary>
        [SerializeField]
        private Transform m_Target = null;

        /// <summary>
        /// 追跡する速度
        /// </summary>
        [SerializeField, Range(0, 1)]
        private float m_Speed = 1.0f;

        /// <summary>
        /// 追跡する対象を取得または設定します。
        /// </summary>
        public Transform Target
        {
            get { return m_Target; }
            set { m_Target = value; }
        }

        /// <summary>
        /// 追跡する速度を取得または設定します。
        /// </summary>
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        /// <summary>
        /// 毎フレーム呼び出されます。
        /// </summary>
        private void Update()
        {
            if (m_Target != null)
            {
                var t = Mathf.Lerp(Time.deltaTime, 1.0f, m_Speed) * m_Speed;
                transform.position = Vector3.Lerp(transform.position, m_Target.position, t);
                transform.rotation = Quaternion.Lerp(transform.rotation, m_Target.rotation, t);
            }
        }

        /// <summary>
        /// 追従する対象の位置に即座に移動します。
        /// </summary>
        public void MoveImmediately()
        {
            if (m_Target != null)
            {
                transform.position = m_Target.position;
                transform.rotation = m_Target.rotation;
            }
        }
    }
}