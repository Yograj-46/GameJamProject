using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool equip;
		public bool lightAttack;
		public bool heavyAttack;
		public bool pickup;
		public bool kick;
		public bool summon;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnEquip(InputValue value)
		{
			EquipInput(value.isPressed);
		}
		public void OnLightAttack(InputValue value)
		{
			LightAttackInput(value.isPressed);
		}
		public void OnHeavyAttack(InputValue value)
		{
			HeavyAttackInput(value.isPressed);
		}
		public void OnPickup(InputValue value)
		{
			PickupInput(value.isPressed);
		}
        public void OnKick(InputValue value)
        {
            KickpInput(value.isPressed);
        }
		public void OnSummon(InputValue value){
			SummonInput(value.isPressed);
		}
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void EquipInput(bool equipState)
		{
			equip = equipState;
		}
		public void LightAttackInput(bool lightAttackState)
		{
			lightAttack = lightAttackState;
		}
		public void HeavyAttackInput(bool heavyAttackState)
		{
			heavyAttack = heavyAttackState;
		}
		public void PickupInput(bool pickupState)
		{
			pickup = pickupState;
		}
        public void KickpInput(bool kickState)
        {
            kick = kickState;
        }

		public void SummonInput(bool summonState){
			summon = summonState;
		}

        private void OnApplicationFocus(bool hasFocus)
		{
			//SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}