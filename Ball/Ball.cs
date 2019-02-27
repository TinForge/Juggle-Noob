using System.Collections.Generic;
using UnityEngine;

public class Ball : StateListener
{
    [Header("Game")]
    private bool active;
    [SerializeField] private Vector3 startPosition;
    private static List<Ball> Balls = new List<Ball>();

    [Header("Appearance")]
    private SpriteRenderer sr;
    private CircleCollider2D cc;

    [Header("Physics")]
    private Rigidbody2D rb;
    private TargetJoint2D tj;
    private RaycastHit2D hit;

    [Header("Fingers")]
    private static HashSet<int> ActiveFingers = new HashSet<int>();
    private int fingerID = -1;

    void Awake()
    {
        base.Awake();
        Balls.Add(this);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        rb.drag = Data.Drag;
    }

    #region State

    protected override void Activate()
    {
        active = true;

        sr.sprite = BallDatabase.GetSkin();

        transform.rotation = Quaternion.identity;
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    protected override void Deactivate()
    {
        active = false;
        ActiveFingers.Clear();
        fingerID = -1;
        if (tj != null)
            Destroy(tj);
    }

    #endregion

    #region Fingers

    private static void AssignFinger(int id)
    {
        ActiveFingers.Add(id);
    }

    private static void UnassignFinger(int id)
    {
        ActiveFingers.Remove(id);
    }

    private static bool isFingerUsed(int id)
    {
        if (ActiveFingers.Contains(id))
            return true;
        else
            return false;
    }

    #endregion

    #region Physics

    void Update()
    {
        if (!active)
            return;

        foreach (Touch touch in TouchSystem.GetTouches())
        {
            if (true)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 pos3 = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 pos = pos3;
                    if (IsContact(touch) && !isFingerUsed(touch.fingerId))
                    { //check touching us, and finger is used
                        fingerID = touch.fingerId;
                        AssignFinger(touch.fingerId);
                        //rb.velocity = Vector2.zero;
                        rb.AddForceAtPosition(new Vector2(transform.position.x - pos.x * 3, 25), pos, ForceMode2D.Impulse);
                        AudioPlayer.Play(AudioPlayer.BallTouch);
                        ScoreKeeper.SetScore();
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (touch.fingerId == fingerID)
                    { //destroy joint
                        fingerID = -1;
                        UnassignFinger(touch.fingerId);
                        AudioPlayer.Play(AudioPlayer.BallCollide);
                    }
                }
                else
                {

                    if (touch.phase == TouchPhase.Moved)
                    {
                        Vector3 pos3 = Camera.main.ScreenToWorldPoint(touch.position);
                        Vector2 pos = pos3;
                        if (tj == null && IsContact(touch) && !isFingerUsed(touch.fingerId))
                        { //check touching us, and finger is used
                            fingerID = touch.fingerId;
                            AssignFinger(touch.fingerId);
                            tj = AddJoint(touch.position);
                            rb.velocity = Vector2.zero;
                            AudioPlayer.Play(AudioPlayer.BallTouch);
                        }
                        else if (tj != null && isFingerUsed(touch.fingerId) && touch.fingerId == fingerID)
                        { //check finger is used, and belongs to us
                            rb.AddTorque((pos.magnitude - tj.target.magnitude) * 50); //50 = Rotate multiplier
                            tj.target = pos;
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        if (touch.fingerId == fingerID)
                        { //destroy joint
                            fingerID = -1;
                            UnassignFinger(touch.fingerId);
                            Destroy(tj);
                            ScoreKeeper.SetScore();
                            AudioPlayer.Play(AudioPlayer.BallCollide);
                        }
                    }
                }
            }
        }
    }

    private bool IsContact(Touch touch)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
        float distance = Vector2.Distance(transform.position, pos);

        if (distance < cc.radius + Data.Handicap)
        {
            foreach (Ball b in Balls)
            {
                if (Vector2.Distance(b.transform.position, pos) < distance)
                    return false;
            }
            return true;
        }
        return false;
    }

    private TargetJoint2D AddJoint(Vector2 pos)
    {
        TargetJoint2D tj = gameObject.AddComponent<TargetJoint2D>();
        tj.maxForce = Data.MaxForce;
        tj.frequency = Data.Frequency;
        tj.dampingRatio = Data.Dampening;

        return tj;
    }

    #endregion

}
