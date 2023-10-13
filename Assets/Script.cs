using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    float tempo;
    bool puoiSaltare = true;
    public float accelerazione = 15, salto = 2.5f, massimoVelocita = 6;
    Vector3 posizioneIniziale;

    void Start() => posizioneIniziale = transform.position;

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + (Vector2.down * 1.1f), Vector2.down, 1);
        if (GetComponent<Rigidbody2D>().velocity.x < massimoVelocita && hit.collider != null)
        {
            if (Input.GetAxis("Horizontal") > 0)
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * accelerazione);
            else if (Input.GetAxis("Horizontal") < 0)
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * accelerazione);
        }

        if (puoiSaltare)
            if (Input.GetAxis("Jump") > 0)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * salto, ForceMode2D.Impulse);
                puoiSaltare = false;
                tempo = 0;
            }

        if (Input.GetButton("Cancel"))
        {
            transform.position = posizioneIniziale;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) => tempo = Time.time;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!puoiSaltare)
            if (Time.time > tempo + .2f)
                puoiSaltare = true;

    }
}

