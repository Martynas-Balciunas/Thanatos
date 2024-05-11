using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float yOffset;
    private void LateUpdate()
    {
        transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + yOffset, transform.position.z);
    }
}
