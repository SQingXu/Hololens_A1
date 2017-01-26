using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour {
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

	// Use this for initialization
	void Start () {
        keywords.Add("Cube", () =>
        {
            this.BroadcastMessage("Cube");
        });
        keywords.Add("Back", () =>
        {
            this.BroadcastMessage("Undo");
        });
        keywords.Add("Sphere", () =>
        {
            this.BroadcastMessage("Sphere");
        });
        keywords.Add("Clear", () =>
        {
            this.BroadcastMessage("Clear");
        });
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
