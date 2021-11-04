using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "ScriptableObjects/Order", order = 1)]
public class Order : ScriptableObject {
    public OrderType type;
    [SerializeField, TextArea] private string keywords;
    [HideInInspector] public List<string> words;
    public Action_Voleur action;

    public void Initialize() {
        words = new List<string>();
        string[] splitted = keywords.Split(',');
        for (int i = 0; i < splitted.Length; i++) {
            splitted[i] = splitted[i].Trim('\n');
            splitted[i] = splitted[i].ToLower();
            //splitted[i] = Verbose(splitted[i]);
            words.AddRange(Verbose(splitted[i]));
        }
    }

    private List<string> Verbose(string sentence) {
        List<string> output = new List<string>();
        int[] startIndexes = AllIndexOf(sentence, '[');
        //int startIndex = sentence.IndexOf('[');
        int[] endIndexes = AllIndexOf(sentence, ']');
        if (startIndexes.Length < 0 || endIndexes.Length < 0) { output.Add(sentence); return output; }
        for (int j = 0; j < startIndexes.Length; j++) {
            string brackets = sentence.Substring(startIndexes[j] + 1, endIndexes[j] - startIndexes[j] - 1);
            string[] options = brackets.Split('/');
            for (int i = 0; i < options.Length; i++) {
                string verb = sentence.Substring(0, startIndexes[j]) + options[i] + sentence.Substring(endIndexes[j] + 1);
                Debug.Log(verb);
                output.Add(sentence);
            }
        }
        //output.Add(sentence);
        return output;
    }

    private int[] AllIndexOf(string sentence, char character) {
        List<int> indexes = new List<int>();
        int lastIndex = 0;
        int index = 0;
        int dontcrashpls = 0;
        while (lastIndex < sentence.Length && index >= 0 && dontcrashpls < 10) {
            index = sentence.IndexOf(character, lastIndex);
            if (index == -1) { continue; }
            lastIndex = index + 1;
            indexes.Add(index);
            dontcrashpls++;
        }
        return indexes.ToArray();
    }

    public bool ContainsKeyword(string keyword) {
        if (words == null || words.Count == 0) {
            Initialize();
        }

        if (words.Contains(keyword.ToLower())) {
            return true;
        }
        return false;
    }
}
