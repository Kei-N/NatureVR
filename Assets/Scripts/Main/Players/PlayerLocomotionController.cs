using UnityEngine;

namespace NatureVR.Player
{
    /// <summary>
    /// コントローラで対象を移動させる機能を提供します。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PlayerLocomotionController : MonoBehaviour
    {
        /// <summary>
        /// カメラ
        /// </summary>
        [SerializeField]
        private Camera m_Camera = null;

        /// <summary>
        /// 移動する対象
        /// </summary>
        [SerializeField]
        private Transform m_LocomotionTarget = null;

        /// <summary>
        /// 移動量(一秒間に移動する距離)
        /// </summary>
        [SerializeField, Range(0, 10)]
        private float m_MoveAmount = 1.0f;

        /// <summary>
        /// 加速量(ジョイスティックが押下されているときの加速度)
        /// </summary>
        [SerializeField, Range(0, 100)]
        private float m_MoveAcceleration = 10.0f;

        /// <summary>
        /// 回転量(一回の入力で回転する角度)
        /// </summary>
        [SerializeField, Range(0, 360)]
        private float m_RotateAmount = 10.0f;

        /// <summary>
        /// 毎フレーム呼び出されます。
        /// </summary>
        private void Update()
        {
            // 左ジョイスティックのY軸が入力されている場合は位置を上下に移動
            var leftAcceleration =
                OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch) ? m_MoveAcceleration : 1;
            var leftVertical =
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.LTouch) || (UnityEngine.Input.GetKey(KeyCode.LeftControl) && UnityEngine.Input.GetKey(KeyCode.UpArrow)) ? 1 :
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.LTouch) || (UnityEngine.Input.GetKey(KeyCode.LeftControl) && UnityEngine.Input.GetKey(KeyCode.DownArrow)) ? -1 :
                0;
            if (Mathf.Abs(leftVertical) > 0)
            {
                var moveAmount = m_MoveAmount * leftVertical * leftAcceleration * Time.deltaTime;
                var moveForward = m_LocomotionTarget.InverseTransformDirection(m_Camera.transform.up);
                var translation = new Vector3(0, moveForward.y, 0).normalized * moveAmount;
                m_LocomotionTarget.Translate(translation, Space.Self);
            }

            // 右ジョイスティックのY軸が入力されている場合は位置を前後左右に移動
            var rightAcceleration =
                OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch) ? m_MoveAcceleration : 1;
            var rightVertical =
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.RTouch) || (UnityEngine.Input.GetKey(KeyCode.RightControl) && UnityEngine.Input.GetKey(KeyCode.UpArrow)) ? 1 :
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch) || (UnityEngine.Input.GetKey(KeyCode.RightControl) && UnityEngine.Input.GetKey(KeyCode.DownArrow)) ? -1 :
                0;
            if (Mathf.Abs(rightVertical) > 0)
            {
                var moveAmount = m_MoveAmount * rightVertical * rightAcceleration * Time.deltaTime;
                var moveForward = m_LocomotionTarget.InverseTransformDirection(m_Camera.transform.forward);
                var translation = new Vector3(moveForward.x, 0, moveForward.z).normalized * moveAmount;
                m_LocomotionTarget.Translate(translation, Space.Self);
            }

            // 右ジョイスティックのX軸が入力されている場合は位置を左右に回転
            var rightHorizontal =
                OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch) || (UnityEngine.Input.GetKey(KeyCode.RightControl) && UnityEngine.Input.GetKeyDown(KeyCode.RightArrow)) ? 1 :
                OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch) || (UnityEngine.Input.GetKey(KeyCode.RightControl) && UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow)) ? -1 :
                0;
            if (Mathf.Abs(rightHorizontal) > 0)
            {
                var rotateAmount = m_RotateAmount * rightHorizontal;
                m_LocomotionTarget.RotateAround(m_Camera.transform.position, m_LocomotionTarget.up, rotateAmount);
            }
        }

        /// <summary>
        /// コントローラの位置をローカル座標の原点にリセットします。
        /// </summary>
        public void ResetOrigin()
        {
            m_LocomotionTarget.localPosition = Vector3.zero;
            m_LocomotionTarget.localRotation = Quaternion.identity;
        }
    }
}