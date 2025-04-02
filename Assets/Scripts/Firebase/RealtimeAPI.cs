using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class RealtimeAPI : MonoBehaviour
{
    [Header("Firebase Settings")]
    public string databaseUrl = "https://game-5f9d8-default-rtdb.asia-southeast1.firebasedatabase.app/";
    public string authToken = ""; // Optional

    private string FormatUrl(string path, Dictionary<string, string> queryParams = null)
    {
        string url = $"{databaseUrl.TrimEnd('/')}/{path.Trim('/')}.json";

        List<string> queryList = new List<string>();
        if (!string.IsNullOrEmpty(authToken))
            queryList.Add("auth=" + authToken);

        if (queryParams != null)
        {
            foreach (var kvp in queryParams)
                queryList.Add($"{kvp.Key}={UnityWebRequest.EscapeURL(kvp.Value)}");
        }

        if (queryList.Count > 0)
            url += "?" + string.Join("&", queryList);

        return url;
    }

    // GET
    public void Get(string path, System.Action<string> onSuccess, Dictionary<string, string> queryParams = null)
    {
        string url = FormatUrl(path, queryParams);
        StartCoroutine(SendRequest(UnityWebRequest.Get(url), onSuccess));
    }

    // PUT
    public void Put(string path, object data, System.Action<string> onSuccess = null)
    {
        string url = FormatUrl(path);
        string jsonData = JsonConvert.SerializeObject(data);

        UnityWebRequest request = UnityWebRequest.Put(url, jsonData);
        request.method = UnityWebRequest.kHttpVerbPUT;
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(SendRequest(request, onSuccess));
    }

    // POST
    public void Post(string path, object data, System.Action<string> onSuccess = null)
    {
        string url = FormatUrl(path);
        string jsonData = JsonConvert.SerializeObject(data);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(SendRequest(request, onSuccess));
    }

    // PATCH
    public void Patch(string path, object data, System.Action<string> onSuccess = null)
    {
        string url = FormatUrl(path);
        string jsonData = JsonConvert.SerializeObject(data);

        UnityWebRequest request = new UnityWebRequest(url, "PATCH");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(SendRequest(request, onSuccess));
    }

    // DELETE
    public void Delete(string path, System.Action<string> onSuccess = null)
    {
        string url = FormatUrl(path);
        UnityWebRequest request = UnityWebRequest.Delete(url);
        StartCoroutine(SendRequest(request, onSuccess));
    }

    // CHECK if a specific field is updated
    public void IsFieldUpdated<T>(string path, string fieldName, T oldValue, System.Action<bool> resultCallback)
    {
        Get(path, (response) =>
        {
            if (string.IsNullOrEmpty(response))
            {
                resultCallback?.Invoke(false);
                return;
            }

            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                if (dict != null && dict.ContainsKey(fieldName))
                {
                    object current = dict[fieldName];
                    string currentStr = JsonConvert.SerializeObject(current);
                    string oldStr = JsonConvert.SerializeObject(oldValue);

                    resultCallback?.Invoke(currentStr != oldStr);
                }
                else
                {
                    resultCallback?.Invoke(true); // Treat missing field as updated
                }
            }
            catch
            {
                resultCallback?.Invoke(false);
            }
        });
    }

    // Reusable Coroutine
    private IEnumerator SendRequest(UnityWebRequest request, System.Action<string> onSuccess)
    {
        yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
        if (request.result == UnityWebRequest.Result.Success)
#else
        if (!request.isNetworkError && !request.isHttpError)
#endif
        {
            onSuccess?.Invoke(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"Firebase Error: {request.error}");
            onSuccess?.Invoke(null);
        }
    }

    // Optional: Firebase query param builder
    public Dictionary<string, string> BuildQuery(
        string orderBy = null,
        string equalTo = null,
        string startAt = null,
        string endAt = null,
        int? limitToFirst = null,
        int? limitToLast = null)
    {
        Dictionary<string, string> query = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(orderBy)) query["orderBy"] = $"\"{orderBy}\"";
        if (!string.IsNullOrEmpty(equalTo)) query["equalTo"] = $"\"{equalTo}\"";
        if (!string.IsNullOrEmpty(startAt)) query["startAt"] = $"\"{startAt}\"";
        if (!string.IsNullOrEmpty(endAt)) query["endAt"] = $"\"{endAt}\"";
        if (limitToFirst.HasValue) query["limitToFirst"] = limitToFirst.Value.ToString();
        if (limitToLast.HasValue) query["limitToLast"] = limitToLast.Value.ToString();

        return query;
    }
}


#region
/*
 
public class FirebaseTest : MonoBehaviour
{
    public FirebaseDatabase firebase;

    void Start()
    {
        // PUT data
        firebase.Put("users/user1", new { username = "Shanto", score = 100 });

        // GET data
        firebase.Get("users/user1", (response) =>
        {
            Debug.Log("GET Response: " + response);
        });

        // PATCH score
        firebase.Patch("users/user1", new { score = 150 });

        // POST new user
        firebase.Post("users", new { username = "Newbie", score = 10 });

        // DELETE user
        firebase.Delete("users/user1");

        // Check if field "score" is updated
        firebase.IsFieldUpdated("users/user2", "score", 100, (updated) =>
        {
            Debug.Log("Score updated? " + updated);
        });
    }
}


 */



#endregion

