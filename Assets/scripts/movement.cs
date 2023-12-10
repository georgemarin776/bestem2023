using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Movement : MonoBehaviour
{
    private const float DIRECTION_TIME_TRESHOLD = 1f; 

    public bool isInAttack = false;
    public bool isInDefense = false;

    public bool isAttakingInDirection = false;
    public bool isGuardingInDirection = false;
    public float attackTime;
    public float defenseTime;

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;

    public bool[] isGuarding = new bool[4];

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        { 
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = 0;
        if (!isInAttack && !isInDefense)
        {
            if (Input.GetKey(KeyCode.A))
                inputX = -1;
            else if (Input.GetKey(KeyCode.D))
                inputX = 1;
        }

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Jump
        if (Input.GetKeyDown(KeyCode.W) && m_grounded && !isAttakingInDirection)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttakingInDirection = true;
            // Get current time, so it refreshes back to false when not pressing a direction after n seconds
            attackTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isGuardingInDirection = true;

            defenseTime = Time.time;    
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);

        if (Time.time - attackTime > DIRECTION_TIME_TRESHOLD)
        {
            isAttakingInDirection = false;
        }

        if (Time.time - defenseTime > DIRECTION_TIME_TRESHOLD)
        {
            isGuardingInDirection = false;
        }
    }
}
