using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public LayerMask spotMasks;
    public MoveSpot actualSpot = null;
    public MoveSpot nextSpot = null;
    public float speed = 0f;
    public Tools.Directions orientation = Tools.Directions.UP;
    public ActionsManager actionsManager = null;
    public List<Action_Voleur> actions = null;
    public List<Action_Voleur> orders = null;
    public RecognitionVoice voice = null;
    public GameObject viewArrow = null;
    public Arduino_test arduino = null;
    private bool onAction = false;

    private Tools.Delegate<MoveSpot> onArrivedSpot;
    [SerializeField] private Material _playerFrontMaterial = null;
    [SerializeField] private Material _playerBackMaterial = null;
    [SerializeField] private Texture _playerTelTexture = null;
    [SerializeField] private Animator animatorFront;
    [SerializeField] private Animator animatorBack;
    private bool oneTime = false;

    #region Coroutine
    private Coroutine moveRoutine = null;
    private Coroutine rotationRoutine = null;
    #endregion

    private void Start() {
        onArrivedSpot += SetActualSpot;
        onArrivedSpot += ResetNextSpot;
        orders = new List<Action_Voleur>();
        SetRotation(orientation);
    }

    private void Update() {
        if (actualSpot.phone != null && !onAction) {
            if (actualSpot.phone.isDringDring) {
                voice.StartDictationEngine();
                actualSpot.phone.OnAllo();
            } else {
                if (!arduino.OnCall && voice.actions.Count > 0) {
                    onAction = true;
                    voice.CloseDictationEngine();
                    actionsManager.ExecuteActions(voice.actions);
                    voice.actions.Clear();
                }
            }
        } else if (actualSpot.phone == null && !onAction && actionsManager.actionsDone.Count > 0) {
            actionsManager.GoBackToBeginning();
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            orders.Add(actions[0]);
            Debug.Log("Up");
        } else if (Input.GetKeyDown(KeyCode.D)) {
            orders.Add(actions[1]);
            Debug.Log("Right");
        } else if (Input.GetKeyDown(KeyCode.S)) {
            orders.Add(actions[2]);
            Debug.Log("Down");
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            orders.Add(actions[3]);
            Debug.Log("Left");
        } else if (Input.GetKeyDown(KeyCode.A)) {
            actionsManager.ExecuteActions(orders);
            orders.Clear();
            Debug.Log("Execute");
        } else if (Input.GetKeyDown(KeyCode.E)) {
            if (!voice.recording) {
                voice.StartDictationEngine();
            } else {
                voice.CloseDictationEngine();
            }
        } else if (Input.GetKeyDown(KeyCode.R)) {
            onAction = true;
            actionsManager.ExecuteActions(voice.actions);
            voice.actions.Clear();
        }
    }

    public void WorkDone() {
        if (actualSpot == null) { return; }
        onAction = false;
    }

    #region Movements

    public MoveSpot SpotInDirection(Tools.Directions direction) {
        if (actualSpot == null) { return null; }
        if (actualSpot.spotDirections[(int)direction] == null) { return null; }
        return actualSpot.spotDirections[(int)direction];
    }

    public Coroutine MoveToSpotDirection(Tools.Directions direction) {
        MoveSpot spot = SpotInDirection(direction);
        if (SpotInDirection(direction) != null) {
            SetRotation(direction);
            return MoveToSpot(spot);
        }
        return null;
    }

    public Coroutine MoveToSpot(MoveSpot spot) {
        if (moveRoutine != null) { StopCoroutine(moveRoutine); }
        nextSpot = spot;
        moveRoutine = StartCoroutine(IMoveTo(spot, speed));
        return moveRoutine;
    }

    private IEnumerator IMoveTo(MoveSpot spot, float speed) {
        Vector2 position = spot.transform.position.To2D();
        while (Vector2.Distance(transform.position.To2D(), position) > 0.03f) {
            Vector2 direction = (position - transform.position.To2D()).normalized;
            //transform.position += direction.To3D() * speed;
            transform.position = Vector3.MoveTowards(transform.position, spot.transform.position.Override(transform.position.y), speed);
            if (!oneTime)
            {
                animatorFront.SetTrigger("isWalking");
                animatorBack.SetTrigger("isWalking");
                oneTime = true;
            }
            yield return new WaitForEndOfFrame();
        }
        onArrivedSpot(spot);
        animatorFront.SetTrigger("isWalking");
        animatorBack.SetTrigger("isWalking");
        oneTime = false;
    }

    public void OnPhonePos(Phone phone)
    {
        if (phone.isDringDring)
        {
            _playerFrontMaterial.mainTexture = _playerTelTexture;
            _playerBackMaterial.mainTexture = _playerTelTexture;
        }
    }

    #endregion

    public void SetActualSpot(MoveSpot spot) {
        actualSpot = spot;
    }

    public void ResetNextSpot(MoveSpot spot) {
        nextSpot = null;
    }

    public void SetRotation(Tools.Directions direction) {
        //viewArrow.transform.rotation = Quaternion.LookRotation(direction.Vector());
        ArrowRotation(direction);
        orientation = direction;
    }

    private void ArrowRotation(Tools.Directions direction) {
        if (rotationRoutine != null) { StopCoroutine(rotationRoutine); }
        //Quaternion rotation = Quaternion.LookRotation(direction.Vector());
        Quaternion rotation = Quaternion.LookRotation(direction.Vector(), Vector3.up);
        rotationRoutine = StartCoroutine(IArrowRotation(rotation, 0.2f));
    }

    private IEnumerator IArrowRotation(Quaternion quaternion, float time) {
        float timePassed = 0;
        Quaternion baseRot = viewArrow.transform.rotation;
        while (timePassed < time) {
            viewArrow.transform.rotation = Quaternion.Slerp(baseRot, quaternion, timePassed / time);
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
        }
        viewArrow.transform.rotation = quaternion;
    }
}

public static class Tools {
    public enum Axis { X, Y, Z, W }
    public enum Directions { UP, RIGHT, DOWN, LEFT }

    public delegate void Delegate();
    public delegate void Delegate<T>(T arg);

    public static Directions Inverse(this Directions direction) {
        return (Directions)(((int)direction + 2) % 4);
    }

    public static Directions Add(this Directions direction, Directions addDirection) {
        return (Directions)(((int)direction + (int)addDirection) % 4);
    }

    public static IEnumerator Wait(float time, Tools.Delegate callback) {
        yield return new WaitForSeconds(time);
        callback();
    }

    public static IEnumerator Wait(IEnumerator enumerator, Tools.Delegate callback) {
        yield return enumerator;
        callback();
    }

    public static IEnumerator Wait(Coroutine enumerator, Tools.Delegate callback) {
        yield return enumerator;
        callback();
    }

    #region Vector Extentions 

    public static Vector2 To2D(this Vector3 vector, Axis axis = Axis.Y) {
        switch (axis) {
            case Axis.X:
                return new Vector2(vector.y, vector.z);
            case Axis.Y:
                return new Vector2(vector.x, vector.z);
            case Axis.Z:
                return new Vector2(vector.x, vector.y);
            default:
                return new Vector2(vector.x, vector.z);
        }
    }

    public static Vector3 To3D(this Vector2 vector, float value = 0f, Axis axis = Axis.Y) {
        switch (axis) {
            case Axis.X:
                return new Vector3(value, vector.x, vector.y);
            case Axis.Y:
                return new Vector3(vector.x, value, vector.y);
            case Axis.Z:
                return new Vector3(vector.x, vector.y, value);
            default:
                return new Vector3(vector.x, value, vector.y);
        }
    }

    public static Vector3 Vector(this Directions direction) {
        switch (direction) {
            case Directions.UP:
                return new Vector3(0, 0, 1);
            case Directions.RIGHT:
                return new Vector3(1, 0, 0);
            case Directions.DOWN:
                return new Vector3(0, 0, -1);
            case Directions.LEFT:
                return new Vector3(-1, 0, 0);
            default:
                return Vector3.zero;
        }
    }

    public static Vector3 Override(this Vector3 vector, float value = 0f, Axis axis = Axis.Y) {
        switch (axis) {
            case Axis.X:
                vector.Set(value, vector.y, vector.z);
                break;
            case Axis.Y:
                vector.Set(vector.x, value, vector.z);
                break;
            case Axis.Z:
                vector.Set(vector.x, vector.y, value);
                break;
            default:
                vector.Set(vector.x, vector.y, vector.z);
                break;
        }
        return vector;
    }

    #endregion
}
