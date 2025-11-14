using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 moveInput = new Vector2(x, 0);
        player.Move(moveInput);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.TryDash(x);
        }
        if (Input.GetButtonDown("Jump"))
        {
            player.Jump();
        }

    }
}
