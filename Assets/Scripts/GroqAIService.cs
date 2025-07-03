using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class GroqAIService : MonoBehaviour
{
    public static GroqAIService Instance;

    [Header("Groq API Settings")]
    public string apiKey = "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";  
    private string endpoint = "https://api.groq.com/openai/v1/chat/completions";
    public string model = "llama-3.1-8b-instant";  

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [System.Serializable]
    public class ChatMessage
    {
        public string role;
        public string content;

        public ChatMessage(string role, string content)
        {
            this.role = role;
            this.content = content;
        }
    }

    [System.Serializable]
    public class ChatRequest
    {
        public string model;
        public List<ChatMessage> messages;
        public float temperature = 0.7f;

        public ChatRequest(string model, List<ChatMessage> messages)
        {
            this.model = model;
            this.messages = messages;
        }
    }

    [System.Serializable]
    public class ChatChoice
    {
        public ChatMessage message;
    }

    [System.Serializable]
    public class ChatResponse
    {
        public List<ChatChoice> choices;
    }

    public IEnumerator SendMessageToAI(List<ChatMessage> messages, System.Action<string> onResponse)
    {
        Debug.Log("Sending request to Groq...");
        Debug.Log("Using API Key: " + apiKey);

        ChatRequest request = new ChatRequest(model, messages);
        string jsonData = JsonConvert.SerializeObject(request);

        UnityWebRequest webRequest = new UnityWebRequest(endpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string json = webRequest.downloadHandler.text;
            Debug.Log("Groq Response: " + json);
            ChatResponse response = JsonConvert.DeserializeObject<ChatResponse>(json);
            onResponse?.Invoke(response.choices[0].message.content.Trim());
        }
        else
        {
            Debug.LogError("Groq API Error: " + webRequest.error);
            Debug.LogError("Groq API Error: " + webRequest.responseCode + " | " + webRequest.downloadHandler.text);
            onResponse?.Invoke("Sorry, I had trouble responding.");
        }
    }
}
