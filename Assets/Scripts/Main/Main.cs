using Cysharp.Threading.Tasks;
using NatureVR.Audio;
using NatureVR.Data;
using NatureVR.Input;
using NatureVR.UI;
using NatureVR.XR;
using System;
using UnityEngine;

namespace NatureVR
{
    /// <summary>
    /// プログラムのエントリーポイントを定義します。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Main : MonoBehaviour
    {
        /// <summary>プレイヤー</summary>
        [SerializeField] private Player.PlayerController m_Player = null;

        /// <summary>音響マネージャ</summary>
        [SerializeField] private AudioManager m_AudioManager = null;

        /// <summary>データマネージャ</summary>
        [SerializeField] private DataManager m_DataManager = null;

        /// <summary>入力マネージャ</summary>
        [SerializeField] private InputManager m_InputManager = null;

        /// <summary>UIマネージャ</summary>
        [SerializeField] private UIManager m_UIManager = null;

        /// <summary>XRマネージャ</summary>
        [SerializeField] private XRManager m_XRManager = null;


        /// <summary>
        /// エントリーポイントです。
        /// </summary>
        private async UniTask Start()
        {
            try
            {
                await InitializeAsync();

                //while (true)
                {
                    // 処理を書く
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// 初期化処理を実行します。
        /// </summary>
        private async UniTask InitializeAsync()
        {
            m_AudioManager.Initialize();

            m_DataManager.Initialize();

            m_InputManager.Initialize();

            m_UIManager.Initialize();

            m_XRManager.Initialize();

            await UniTask.Yield();
        }

    }
}