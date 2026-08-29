
using Scripts.Systems;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInput: MonoBehaviour
    {
        public int playerID;

        private RotatePlayerCamera rotatePlayerCamera;

        private void Awake()
        {
           rotatePlayerCamera = GameObject.Find("FocalPoint").GetComponent<RotatePlayerCamera>();
        }

        public void Update()
        {
            int moveIntent = Mathf.RoundToInt(Input.GetAxis("Vertical"));            
            
            float rotate = Input.GetAxis("Horizontal");
            rotatePlayerCamera.Rotate(rotate);

            Launcher.Instance.MovementSystem.SetMoveIntentByEntity(playerID, rotatePlayerCamera.GetForward(), moveIntent);

        }

        public void SetPlayerID(int playerID)
        {
            this.playerID = playerID;
        }
        
    }
}
