using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NatureVR.Utility
{
    public class EditorModeSetting : MonoBehaviour
    {
        [SerializeField] private OVRCameraRig OVRCameraRig;
        [SerializeField] private StandaloneInputModule StandaloneInputModule;
        //[SerializeField] private OVRInputModuleCustom OVRInputModuleCustom;
        [SerializeField] private GraphicRaycaster GraphicRaycaster;
        [SerializeField] private OVRRaycaster OVRRaycaster;

#if UNITY_EDITOR
        void Awake()
        {
            const bool editorMode = true;
            this.OVRCameraRig.enabled = !editorMode;
            this.StandaloneInputModule.enabled = editorMode;
            //this.OVRInputModuleCustom.enabled = !editorMode;
            this.GraphicRaycaster.enabled = editorMode;
            this.OVRRaycaster.enabled = !editorMode;
        }
#endif

        // Start is called before the first frame update
        void Start()
        {

        }
    }
}
