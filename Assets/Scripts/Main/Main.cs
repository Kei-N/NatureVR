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
    /// �v���O�����̃G���g���[�|�C���g���`���܂��B
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Main : MonoBehaviour
    {
        /// <summary>�v���C���[</summary>
        [SerializeField] private Player.PlayerController m_Player = null;

        /// <summary>�����}�l�[�W��</summary>
        [SerializeField] private AudioManager m_AudioManager = null;

        /// <summary>�f�[�^�}�l�[�W��</summary>
        [SerializeField] private DataManager m_DataManager = null;

        /// <summary>���̓}�l�[�W��</summary>
        [SerializeField] private InputManager m_InputManager = null;

        /// <summary>UI�}�l�[�W��</summary>
        [SerializeField] private UIManager m_UIManager = null;

        /// <summary>XR�}�l�[�W��</summary>
        [SerializeField] private XRManager m_XRManager = null;


        /// <summary>
        /// �G���g���[�|�C���g�ł��B
        /// </summary>
        private async UniTask Start()
        {
            try
            {
                await InitializeAsync();

                //while (true)
                {
                    // ����������
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// ���������������s���܂��B
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