using UnityEngine;

namespace RDFW
{
    public class SetResolution : MonoBehaviour
    {
        public int width=1920;
        public int height=1080;
        public bool isFullScreen=true;
        private void Awake()
        {
            Screen.SetResolution(width, height, isFullScreen);
        }
    }
}

