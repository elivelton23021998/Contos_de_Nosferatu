using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class VooAleatorioMenu : MonoBehaviour
{
    [SerializeField] float idleSpeed, turnSpeed, switchSeconds, idleRatio;
    [SerializeField] Vector2 animSpeedMinMax, moveSpeedMinMax, changeAnimEveryFromTo, changeTargetEveryFromTo;
    [SerializeField] Transform homeTarget, flyingTarget;
    [SerializeField] Vector2 radiusMinMax;
    [SerializeField] Vector2 yMinMax;
    [SerializeField] public bool returnToBase = false;
    [SerializeField] public float randomBaseOffset = 5, delayStart = 0f; float time = 0f;

    private Animator animator;
    private Rigidbody body;
    [System.NonSerialized]
    public float changeTarget = 0f, changeAnim = 0f, timeSinceTarget = 0f, timeSinceAnim = 0f, prevAnim, currentAnim = 0f, prevSpeed, speed, zturn, prevz,
        turnSpeedBackup;
    private Vector3 rotateTarget, position, direction, velocity, randomizedBase;
    private Quaternion lookRotation;
    [System.NonSerialized] public float distanceFromBase, distanceFromTarget;


    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        turnSpeedBackup = turnSpeed;
        direction = Quaternion.Euler(transform.eulerAngles) * (Vector3.forward);
        if (delayStart < 0f) body.velocity = idleSpeed * direction;
    }

    void FixedUpdate()
    {
        BackHome();

        if (delayStart > 0f)
        {
            delayStart -= Time.fixedDeltaTime;
            return;
        }
        //CALCULA DISTANCIA
        distanceFromBase = Vector3.Magnitude(randomizedBase - body.position);
        distanceFromTarget = Vector3.Magnitude(flyingTarget.position - body.position);

        //PERMITE VIRADAS BRUSCAS
        if (returnToBase && distanceFromBase < 10f)
        {
            if (turnSpeed != 300f && body.velocity.magnitude != 0f)
            {
                turnSpeedBackup = turnSpeed;
                turnSpeed = 300f;
            }
            else if (distanceFromBase <= 2f)
            {
                body.velocity = Vector3.zero;
                turnSpeed = turnSpeedBackup;
                return;
            }
        }
        //TEMPO PARA UMA NOVA ANIMA플O
        if (changeAnim < 0f)
        {
            prevAnim = currentAnim;
            currentAnim = ChangeAnim(currentAnim);
            changeAnim = Random.Range(changeAnimEveryFromTo.x, changeAnimEveryFromTo.y);
            timeSinceAnim = 0f;
            prevSpeed = speed;
            if (currentAnim == 0) speed = idleSpeed;
            else speed = Mathf.Lerp(moveSpeedMinMax.x, moveSpeedMinMax.y, (currentAnim - animSpeedMinMax.x) / (animSpeedMinMax.y - animSpeedMinMax.x));
        }
        //TEMPO PARA UM NOVO PONTO ALVO
        if (changeTarget < 0f)
        {
            rotateTarget = ChangeDirection(body.transform.position);
            if (returnToBase) changeTarget = 0.2f; else changeTarget = Random.Range(changeTargetEveryFromTo.x, changeTargetEveryFromTo.y);
            timeSinceTarget = 0f;
        }
        //VIRA QUANDO SE APROXIMA DOS PONTOS ALTOS
        // ToDo: Adjust limit and "exit direction" by object's direction and velocity, instead of the 10f and 1f - this works in my current scenario/scale
        if (body.transform.position.y < yMinMax.x + 10f ||
            body.transform.position.y > yMinMax.y - 10f)
        {
            if (body.transform.position.y < yMinMax.x + 10f) rotateTarget.y = 1f; else rotateTarget.y = -1f;
        }
        zturn = Mathf.Clamp(Vector3.SignedAngle(rotateTarget, direction, Vector3.up), -45f, 45f);

        //ATUALIZA TEMPOS
        changeAnim -= Time.fixedDeltaTime;
        changeTarget -= Time.fixedDeltaTime;
        timeSinceTarget += Time.fixedDeltaTime;
        timeSinceAnim += Time.fixedDeltaTime;

        //ROTA플O
        if (rotateTarget != Vector3.zero) lookRotation = Quaternion.LookRotation(rotateTarget, Vector3.up);
        Vector3 rotation = Quaternion.RotateTowards(body.transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime).eulerAngles;
        body.transform.eulerAngles = rotation;

        //ROTA플O Z
        float temp = prevz;
        if (prevz < zturn) prevz += Mathf.Min(turnSpeed * Time.fixedDeltaTime, zturn - prevz);
        else if (prevz >= zturn) prevz -= Mathf.Min(turnSpeed * Time.fixedDeltaTime, prevz - zturn);

        prevz = Mathf.Clamp(prevz, -45f, 45f);

        body.transform.Rotate(0f, 0f, prevz - temp, Space.Self);

        //MOVIMENTO VOO
        direction = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;
        if (returnToBase && distanceFromBase < idleSpeed)
        {
            body.velocity = Mathf.Min(idleSpeed, distanceFromBase) * direction;
        }
        else body.velocity = Mathf.Lerp(prevSpeed, speed, Mathf.Clamp(timeSinceAnim / switchSeconds, 0f, 1f)) * direction;

        if (body.transform.position.y < yMinMax.x || body.transform.position.y > yMinMax.y)
        {
            position = body.transform.position;
            position.y = Mathf.Clamp(position.y, yMinMax.x, yMinMax.y);
            body.transform.position = position;
        }

    }

    //SELECIONA UMA NOVA ANIMA플O RANDOMICAMENTE
    private float ChangeAnim(float currentAnim)
    {
        float newState;
        if (Random.Range(0f, 1f) < idleRatio) newState = 0f;
        else
        {
            newState = Random.Range(animSpeedMinMax.x, animSpeedMinMax.y);
        }
        if (newState != currentAnim)
        {
            animator.SetFloat("velocVoo", newState);
            if (newState == 0) animator.speed = 1f; else animator.speed = newState;
        }
        return newState;
    }

    //SELECIONA UM NOVO PONTO PARA VOO ALEATORIAMENTE
    private Vector3 ChangeDirection(Vector3 currentPosition)
    {
        Vector3 newDir;
        if (returnToBase)
        {
            randomizedBase = homeTarget.position;
            randomizedBase.y += Random.Range(-randomBaseOffset, randomBaseOffset);
            newDir = randomizedBase - currentPosition;
        }
        else if (distanceFromTarget > radiusMinMax.y)
        {
            newDir = flyingTarget.position - currentPosition;
        }
        else if (distanceFromTarget < radiusMinMax.x)
        {
            newDir = currentPosition - flyingTarget.position;
        }
        else
        {
            float angleXZ = Random.Range(-Mathf.PI, Mathf.PI);

            float angleY = Random.Range(-Mathf.PI / 48f, Mathf.PI / 48f);

            //CALCULA DIRE플O
            newDir = Mathf.Sin(angleXZ) * Vector3.forward + Mathf.Cos(angleXZ) * Vector3.right + Mathf.Sin(angleY) * Vector3.up;
        }
        return newDir.normalized;
    }


    void BackHome()
    {
        time += Time.deltaTime;

        if (time > 25f && time < 43)
        {
            returnToBase = true;
        }

        if (time > 45f)
        {
            returnToBase = false;
            time = 0f;
        }
    }
}