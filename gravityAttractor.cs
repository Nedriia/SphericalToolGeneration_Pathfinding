using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityAttractor : MonoBehaviour
{
    public float gravity = -10f;
    public Rigidbody rb_player;
    public Transform player;

    private void Awake()
    {
        rb_player = player.GetComponent<Rigidbody>();
    }

    public void Attract(Transform body)
    {
        Vector3 targetDir = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        rb_player.AddForce(targetDir * gravity);
    }
}
