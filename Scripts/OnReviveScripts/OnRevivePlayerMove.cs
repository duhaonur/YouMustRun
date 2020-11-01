using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRevivePlayerMove : MonoBehaviour
{
    private CharacterController characterController;

    private Spawner spawner;

    private Vector3 vector;

    private RaycastHit hit;

    GameObject bridgeObject;

    private float speed = 5.0f;
    private float verticalVelocity;
    private float gravity = 12.0f;
    private float jump = 4.5f;
    private float animationDuration = 1.0f;

    private bool isDead = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Time.time < animationDuration)
        {
            characterController.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        DrawRay();

        bridgeObject = GameObject.FindGameObjectWithTag("FireBallSpawnerBridge");

        if (bridgeObject != null)
        {
            spawner = bridgeObject.GetComponent<Spawner>();
        }
    }

    public void FixedUpdate()
    {
        if (isDead)
            return;

        vector = Vector3.zero;

        verticalVelocity -= gravity * Time.deltaTime;

        vector.x = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.y < Screen.height / 2)
            {
                if (Input.mousePosition.x > Screen.width / 2)
                    vector.x = speed / 1.4f;
                else
                    vector.x = -speed / 1.4f;
            }
        }
        vector.y = verticalVelocity;
        vector.z = speed;

        characterController.Move(vector * Time.deltaTime);
    }

    public void JumpButton()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -0.5f;
            verticalVelocity = jump;
        }
    }

    private void DrawRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "SpawnerCollider")
            {
                if (hit.distance < 5)
                {
                    Destroy(hit.collider);
                    spawner.Spawn();
                }
            }
        }
    }
    public void SetSpeed(float speedModifier)
    {
        speed = 2.5f + speedModifier;
    }

    public void Death()
    {
        isDead = true;
        GetComponent<OnReviveScore>().OnDeath();
        GetComponent<OnReviveScore>().AchievementController();
        PlayGamesController.Instance.SaveCloud();
    }
}
