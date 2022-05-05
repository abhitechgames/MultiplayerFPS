using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class MovementController : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 50f;

    public Vector3 move;

    PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine)
            characterController.Move(move * speed * Time.deltaTime);
    }

    public void moveFunc(InputAction.CallbackContext value)
    {
        move.x = value.ReadValue<Vector2>().x;
        move.z = value.ReadValue<Vector2>().y;
    }
}
