using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
    public class SkyboxChangeSimple : UdonSharpBehaviour
    {
        [Header("同期するか")]
        public bool        IsSync;
        [Header("昇順で切り替えるか")]
        public bool        IsReciprocal;
        [Header("切り替えするスカイボックスと最初の番号")]
        public int         m_start_skybox_nomber;        
        public Material[]  m_skyBoxis;
        [Header("切り替え時のSE")]
        public AudioSource m_switch_audioSource;
        public AudioClip   m_switch_audioClip;

        int _index = 0;

        // Start is called before the first frame update
        void Start()
        {
            _index = m_start_skybox_nomber;
            if (_index >= m_skyBoxis.Length)
                _index = m_skyBoxis.Length - 1;
            else if (_index < 0)
                _index = 0;

        }

        public override void Interact()
        {

            if (m_switch_audioSource != null && m_switch_audioClip != null)
                m_switch_audioSource.PlayOneShot(m_switch_audioClip);

            if (IsReciprocal)
            {
                if (!IsSync)
                {
                    _index--;
                    if (_index < 0)
                        _index = m_skyBoxis.Length - 1;
                    RenderSettings.skybox = m_skyBoxis[_index];
                }
                else
                {
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncSub");
                }
            }
            else
            {
                if (!IsSync)
                {
                    _index++;
                    if (_index >= m_skyBoxis.Length)
                        _index = 0;
                    RenderSettings.skybox = m_skyBoxis[_index];
                }
                else
                {
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncSub");
                }

            }
        }
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (!IsSync || Networking.LocalPlayer.playerId == player.playerId)
                return;
            // TODO 同期させる場合は、SyncSkybox_xxを番号分増やしていってください。
            // UDONがアップデートされれば、もっとスマートになります。
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncSkybox_" + _index.ToString());
        }
        public void SyncSkybox_0()
        {
            _index = 0;
            RenderSettings.skybox = m_skyBoxis[0];
        }

        public void SyncSkybox_1()
        {
            _index = 1;
            RenderSettings.skybox = m_skyBoxis[1];
        }

        public void SyncSkybox_2()
        {
            _index = 2;
            RenderSettings.skybox = m_skyBoxis[2];
        }

        public void SyncAdd()
        {
            _index++;
            if (_index >= m_skyBoxis.Length)
                _index = 0;
            RenderSettings.skybox = m_skyBoxis[_index];
        }

        public void SyncSub()
        {
            _index--;
            if (_index < 0)
                _index = m_skyBoxis.Length - 1;
            RenderSettings.skybox = m_skyBoxis[_index];
        }
    }
}
