using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public enum OrderType {
    MOVEMENT,
    ROTATION,
    TIME,
    CONDITION,
    CANCEL
}

public class RecognitionVoice : MonoBehaviour {
    public static RecognitionVoice instance;

    protected DictationRecognizer dictationRecognizer;
    //private string[] tableauString;
    [SerializeField] private Order[] orders;
    public List<Action_Voleur> actions;
    public bool recording = false;
    //private string[] orders;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Recognition voice already exist ! Deleting new one");
            Destroy(gameObject);
        }
    }

    private void Start() {
        for (int i = 0; i < orders.Length; i++) {
            orders[i].Initialize();
        }

        //StartDictationEngine();
    }

    private void OnApplicationQuit() {
        CloseDictationEngine();
    }

    #region DitactionRecognizer

    public void StartDictationEngine() {
        Debug.LogWarning("-> Start dictation");
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationHypothesis += DictationRecognizer_OnDictationHypothesis;
        dictationRecognizer.DictationResult += DictationRecognizer_OnDictationResult;
        dictationRecognizer.DictationComplete += DictationRecognizer_OnDictationComplete;
        dictationRecognizer.DictationError += DictationRecognizer_OnDictationError;
        dictationRecognizer.Start();
        recording = true;
    }

    public void CloseDictationEngine() {
        if (dictationRecognizer != null) {
            Debug.LogWarning("-> End dictation");
            dictationRecognizer.DictationHypothesis -= DictationRecognizer_OnDictationHypothesis;
            dictationRecognizer.DictationComplete -= DictationRecognizer_OnDictationComplete;
            dictationRecognizer.DictationResult -= DictationRecognizer_OnDictationResult;
            dictationRecognizer.DictationError -= DictationRecognizer_OnDictationError;
            if (dictationRecognizer.Status == SpeechSystemStatus.Running) {
                dictationRecognizer.Stop();
            }
            dictationRecognizer.Dispose();
            recording = false;
        }
    }

    private struct ActionsOrdered {
        public Action_Voleur action;
        public int index;

        public ActionsOrdered(Action_Voleur _action, int _index) {
            action = _action;
            index = _index;
        }
    }

    private void DictationRecognizer_OnDictationResult(string text, ConfidenceLevel confidence) {
        Debug.Log(text);
        List<ActionsOrdered> toDo = new List<ActionsOrdered>();
        for (int i = 0; i < orders.Length; i++) {
            for (int j = 0; j < orders[i].words.Count; j++) {
                int index = text.IndexOf(orders[i].words[j]);
                if (index != -1) {
                    if (orders[i].type == OrderType.CANCEL) {
                        actions.Clear();
                        continue;
                    }
                    //actions.Add(orders[i].action);
                    toDo.Add(new ActionsOrdered(orders[i].action, index));
                    text = text.Substring(0, index) + text.Substring(index + orders[i].words[j].Length);
                    i = 0;
                    break;
                    //DictationRecognizer_OnDictationComplete(DictationCompletionCause.Complete);
                }
            }
        }
        toDo = toDo.OrderBy(x => x.index).ToList();
        for (int i = 0; i < toDo.Count; i++) {
            actions.Add(toDo[i].action);
            Debug.Log("+ " + toDo[i].action);
        }
    }

    private void DictationRecognizer_OnDictationComplete(DictationCompletionCause completionCause) {
        switch (completionCause) {
            case DictationCompletionCause.TimeoutExceeded:
            case DictationCompletionCause.PauseLimitExceeded:
            case DictationCompletionCause.Canceled:
            case DictationCompletionCause.Complete:
                // Restart required
                CloseDictationEngine();
                StartDictationEngine();
                break;
            case DictationCompletionCause.UnknownError:
            case DictationCompletionCause.AudioQualityFailure:
            case DictationCompletionCause.MicrophoneUnavailable:
            case DictationCompletionCause.NetworkFailure:
                // Error
                CloseDictationEngine();
                break;
        }
    }

    private void DictationRecognizer_OnDictationError(string error, int hresult) {
        Debug.Log("Dictation error: " + error);
    }

    private void DictationRecognizer_OnDictationHypothesis(string text) {
        Debug.Log("Dictation hypothesis: " + text);
    }

    #endregion
}
