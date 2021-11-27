using UnityEngine;
using UnityEngine.InputSystem;

    public class QuitHandler
    {
        public QuitHandler(InputAction quitAction)
        {
            // An Interaction with the Action has been completed.
            quitAction.performed += QuitAction_performed;
            quitAction.Enable();
        }
        private void QuitAction_performed(InputAction.CallbackContext obj)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
