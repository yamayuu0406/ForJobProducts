using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
    public class ExpansionMenu : UdonSharpBehaviour
    {
        [Header("メニューボタンで表示/非表示するキャンパス")]
        public GameObject m_canves;
        [Header("拡張メニューのトランスフォーム")]
        public Transform m_menu_body;

        bool isShow = false;

        // 外部の操作によって非表示できる
        public void Hide(){
            m_canves.SetActive(false);
            isShow = false;
        }

        void Start()
        {
            m_canves.SetActive(false);
            isShow = false;
        }

        void Update()
        {

            if (
                (InputManager.IsUsingHandController() && Input.GetButtonDown("Oculus_CrossPlatform_Button2") || Input.GetButtonDown("Oculus_CrossPlatform_Button4"))
                || Input.GetKeyDown(KeyCode.Escape)
            )
            {
                if (isShow)
                {
                    m_canves.SetActive(false);
                    isShow = false;
                }
                else
                {
                    var tracking = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
                    m_menu_body.rotation = tracking.rotation;
                    m_menu_body.position = tracking.position + m_menu_body.forward;
                    m_canves.SetActive(true);
                    isShow = true;
                }
            }
        }
    }
}
