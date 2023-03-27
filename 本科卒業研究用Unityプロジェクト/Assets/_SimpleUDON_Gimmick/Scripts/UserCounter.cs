using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace SimpleUDONGimmick
{
    public class UserCounter : UdonSharpBehaviour
    {
        [Header("カウンターを表示するテキスト")]
        public Text m_text;

        [Header("ユーザの単位、「人」とか「User」とか......")]
        public string lot = "人";

        [Header("[Option] Join音")]
        public AudioSource m_audioSource;

        int _count = 0;

        void Start(){
            _count = VRCPlayerApi.GetPlayerCount();
            m_text.text = _count.ToString() + lot;
        }

        public override void OnPlayerJoined(VRCPlayerApi player){
            _count = VRCPlayerApi.GetPlayerCount();
            m_text.text = _count.ToString() + lot;
            if(m_audioSource != null && m_audioSource.isPlaying){
                m_audioSource.Play();
            }
        }

        public override void OnPlayerLeft(VRCPlayerApi player){
            _count--;
            m_text.text = _count.ToString() + lot;
        }
    }
}