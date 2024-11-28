using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Note:
 * The Player Input Manager will automatically spawn a player instance for each connected device. 
 * Ensure your scene supports multiple player instances such as develop a game manager to handle join event
 * Suggestion:
 *  - Configure the player prefab to avoid conflicts (e.g., each player should have unique IDs).
 *  - Use Unity's OnPlayerJoined and OnPlayerLeft events (available on the Player Input Manager) 
 *  to customize the behavior when players join or leave.
 */

public class PlayerJoinedBehaviour : MonoBehaviour
{
    // Require 
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Player Joined: " + playerInput.playerIndex);
        // Customize the spawned player, e.g., set their position or assign unique properties
        playerInput.gameObject.transform.position = GetSpawnPosition(playerInput.playerIndex);
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("Player Left: " + playerInput.playerIndex);
    }

    private Vector3 GetSpawnPosition(int playerIndex)
    {
        // Example spawn positions based on index
        return new Vector3(1f * playerIndex, 5f, 0f);
    }
}
