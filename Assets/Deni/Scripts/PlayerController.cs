using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private LayerMask groundMask;
    private Camera mainCamera;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePosition;
    [SerializeField] float fireRate;
    private float lastShot;
    private Vector3 projectileDirection;
    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject gameOver;
    [SerializeField] Text timer;
    [SerializeField] float time;
    private Rigidbody rb;

    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        ps.playOnAwake = false;
        ps.loop = true;
        audioData = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            timer.text = "TIME: 0:00";
            gameOver.SetActive(true);
            return;
        }
        time -= Time.deltaTime;
        timer.text = "TIME: " + time.ToString("F2");
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        //this.transform.position += new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);

        //Vector3 vector = new Vector3(h, 0f, v);
        //rb.AddForce(vector * speed * Time.deltaTime);
        //rb.MovePosition(rb.position +( vector * speed * Time.deltaTime));

        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
        rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * speed;

        Aim();
        if (lastShot <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                ps.transform.position = firePosition.position;
                ps.Play();
                ps.enableEmission = true;
                Fire();
                audioData.Play();
            }
        }
        else
        {
            ps.Stop();
            lastShot -= Time.deltaTime;
        }
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            var direction = position - transform.position;
            projectileDirection = direction;
            direction.y = 0;

            transform.forward = direction;
            
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }

    private void Fire()
    {
        var shot = Instantiate(projectile, firePosition.position, Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody>().velocity = projectileDirection * projectileSpeed;
        lastShot = fireRate;

    }
}
