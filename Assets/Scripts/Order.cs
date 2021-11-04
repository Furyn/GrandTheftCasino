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
        Debug.LogWarning("// Ini " + this.name);
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
        List<List<string>> verbosed = new List<List<string>>();
        if (startIndexes.Length <= 0 || endIndexes.Length <= 0) { output.Add(sentence); Debug.Log(sentence); return output; }
        for (int j = 0; j < startIndexes.Length; j++) {
            verbosed.Add(new List<string>());
            string brackets = sentence.Substring(startIndexes[j] + 1, endIndexes[j] - startIndexes[j] - 1);
            string[] options = brackets.Split('/');
            for (int i = 0; i < options.Length; i++) {
                verbosed[j].Add(options[i]);
                //string verb = sentence.Substring(0, startIndexes[j]) + options[i] + sentence.Substring(endIndexes[j] + 1);
            }
        }

        List<int> indexes = new List<int>();
        for (int i = 0; i < verbosed.Count; i++) {
            indexes.Add(0);
        }

        while (indexes[0] < verbosed[0].Count) {
            output.Add(sentence.Substring(0, startIndexes[0]) + verbosed[0][indexes[0]]);
            for (int i = 1; i < indexes.Count; i++) {
                string sentenceBegin = sentence.Substring(endIndexes[i - 1] + 1, startIndexes[i] - (endIndexes[i - 1] + 1));
                string sentenceOption = verbosed[i][indexes[i]];
                output[output.Count - 1] += sentenceBegin + sentenceOption;
                //output[output.Count - 1] += sentence.Substring(startIndexes[verbIndex - 1], startIndexes[verbIndex]) + verbosed[verbIndex][j];
            }
            output[output.Count - 1] += sentence.Substring(endIndexes[endIndexes.Length - 1] + 1);
            Debug.Log(output[output.Count - 1]);

            bool exit = false;
            int toUp = indexes.Count - 1;
            while (!exit) {
                indexes[toUp]++;
                if (indexes[toUp] >= verbosed[toUp].Count) {
                    indexes[toUp] = 0;
                    toUp--;
                } else {
                    exit = true;
                }
                if (toUp < 0) {
                    exit = true;
                    indexes[0] = verbosed[0].Count;
                }
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
