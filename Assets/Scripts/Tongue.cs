using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField]
    private float maxRange;

    [SerializeField]
    private float timeToHit;

    [SerializeField]
    private float timeToRetract;

    [SerializeField]
    private AnimationCurve tongueCurve;

    [SerializeField]
    private AnimationCurve tongueRetractCurve;

    [SerializeField]
    private float tongueTipHitboxRadius;

    [SerializeField]
    private float tonguePullStrength;

    [SerializeField]
    private float tonguePullStrengthDecay;

    [SerializeField]
    private float minTonguePullStrength;

    #endregion

    #region Private Variables

    private LineRenderer tongueRenderer;

    private GameObject eatenProjectile;

    private bool tongueOut = false;

    #endregion

    #region Tongue Variables

    private void Awake()
    {
        tongueRenderer = GetComponent<LineRenderer>();
        tongueRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !tongueOut)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * Camera.main.transform.position.z));

            RaycastHit2D tongueTarget = LinecastFromPlayerToPoint(mousePos);

            if (tongueTarget.collider != null) 
            {
                StartCoroutine(ShootTongue(tongueTarget.point));
            }
            else
            {
                StartCoroutine(ShootTongue(mousePos));
            }
        }
    }

    private IEnumerator ShootTongue(Vector2 target)
    {
        tongueOut = true;
        float tongueTimer = timeToHit;
        tongueRenderer.enabled = true;
        while (tongueTimer > 0)
        {
            tongueTimer -= Time.deltaTime;

            Vector2 tongueTipPoint = Vector2.Lerp(transform.position, target, tongueCurve.Evaluate(1 - (tongueTimer / timeToHit)));

            tongueRenderer.SetPositions(new Vector3[] { ((Vector3)transform.position), ((Vector3)tongueTipPoint) });

            yield return new WaitForEndOfFrame();
        }

        RaycastHit2D tongueHit = Physics2D.CircleCast(target, tongueTipHitboxRadius, Vector2.zero, 0);//LinecastFromPlayerToPoint(target);

        if (tongueHit.collider != null)
        {
            if (tongueHit.collider.gameObject.CompareTag("Grappleable"))
            {
                StartCoroutine(LaunchFrog(target));

            }
        }
        else
        {
            StartCoroutine(RetractTongue(target));
        }
    }

    private IEnumerator LaunchFrog(Vector2 target)
    {
        float tempPullStrength = tonguePullStrength;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        

        while(Input.GetButton("Fire1"))
        {
            Vector2 angle = target - (Vector2)transform.position;
            rb.velocity += angle * tempPullStrength;
            tempPullStrength = Mathf.Max(minTonguePullStrength, tempPullStrength - tonguePullStrengthDecay);

            tongueRenderer.SetPositions(new Vector3[] { ((Vector3)transform.position), ((Vector3)target) });
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(RetractTongue(target));
    }

    private IEnumerator RetractTongue(Vector2 origin)
    {
        float tongueTimer = timeToRetract;
        while (tongueTimer > 0)
        {
            tongueTimer -= Time.deltaTime;

            Vector2 tongueTipPoint = Vector2.Lerp(transform.position, origin, tongueRetractCurve.Evaluate(tongueTimer / timeToHit));

            tongueRenderer.SetPositions(new Vector3[] { ((Vector3)transform.position), ((Vector3)tongueTipPoint) });

            yield return new WaitForEndOfFrame();
        }

        tongueRenderer.enabled = false;
        tongueOut = false;
    }

    private RaycastHit2D LinecastFromPlayerToPoint(Vector2 point)
    {
        
        RaycastHit2D hit = Physics2D.Linecast(transform.position, point);
        Debug.DrawLine(transform.position, point, Color.blue, 0.3f);

        return hit;
    }

    #endregion

}
