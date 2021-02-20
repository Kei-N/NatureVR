using NatureVR.Utility;
using UnityEngine;

namespace NatureVR.Player
{
    /// <summary>
    /// プレイヤーを制御する機能を定義します。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// プレイヤーのカメラ
        /// </summary>
        [SerializeField]
        private Camera m_Camera = null;

        /// <summary>
        /// カメラの操作機能
        /// </summary>
        [SerializeField]
        private CameraController m_CameraController = null;

        /// <summary>
        /// プレイヤーの左手の位置
        /// </summary>
        [SerializeField]
        private Transform m_LeftHand = null;

        /// <summary>
        /// プレイヤーの右手の位置
        /// </summary>
        [SerializeField]
        private Transform m_RightHand = null;

        /// <summary>
        /// プレイヤーの初期位置
        /// </summary>
        [SerializeField]
        private Transform m_InitialPlayerPosition = null;

        /// <summary>
        /// カメラを取得します。
        /// </summary>
        public Camera Camera => m_Camera;

        /// <summary>
        /// カメラの操作機能を取得します。
        /// </summary>
        public CameraController CameraController => m_CameraController;

        /// <summary>
        /// プレイヤーの目の位置を取得します。
        /// </summary>
        public Transform EyeTransform => m_Camera.transform;

        /// <summary>
        /// プレイヤーの左手の位置を取得します。
        /// </summary>
        public Transform LeftHandTransform => m_LeftHand;

        /// <summary>
        /// プレイヤーの右手の位置を取得します。
        /// </summary>
        public Transform RightHandTransform => m_RightHand;


        /// <summary>
        /// 初期化処理を実行します。
        /// </summary>
        public void Initialize()
        {
            if (m_InitialPlayerPosition != null) Transfer(m_InitialPlayerPosition);
        }

        /// <summary>
        /// 指定した座標にプレイヤーを転送します。
        /// </summary>
        public void Transfer(Transform parent)
        {
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
