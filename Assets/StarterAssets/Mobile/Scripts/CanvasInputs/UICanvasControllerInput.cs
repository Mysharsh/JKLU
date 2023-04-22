using UnityEngine;
using UIJoystick;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        protected UIVirtualJoystick UIJoystick;

        void Start()
        {
            UIJoystick = GetComponent<UIVirtualJoystick>();
        }

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }
        public void VirtualCrouchInput(bool virtualCrouchState)
        {
            starterAssetsInputs.CrouchInput(virtualCrouchState);
        }

    }

}
