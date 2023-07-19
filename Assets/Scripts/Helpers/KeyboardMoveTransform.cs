using System;
using RamenSea.Foundation3D.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Helpers {
    public class KeyboardMoveTransform: MonoBehaviour {
        [SerializeField] private Transform moveTransform;
        [SerializeField] private float movePerSecond = 1f;
        
        private void Update() {
            if (this.moveTransform == null) {
                return;
            }


            var wIsPressed = Keyboard.current.wKey.isPressed;
            var aIsPressed = Keyboard.current.aKey.isPressed;
            var sIsPressed = Keyboard.current.sKey.isPressed;
            var dIsPressed = Keyboard.current.dKey.isPressed;
            
            Vector2 moveVector = Vector2.zero;
            if (aIsPressed != dIsPressed) {
                moveVector.x = aIsPressed ? -1f : 1f;
            }
            if (wIsPressed != sIsPressed) {
                moveVector.y = wIsPressed ? 1f : -1f;
            }

            this.moveTransform.position = this.moveTransform.position + (moveVector * this.movePerSecond * Time.deltaTime).ToVector3();
        }
    }
}