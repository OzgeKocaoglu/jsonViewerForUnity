/* --------------------------------------------------------------------------
  Title       :  JsonViewer.cs
  Date        :  19 Aug 2024
  Programmer  :  Ozge Kocaoglu
  Package     :  Version 1.0
  Copyright   :  MIT License
-------------------------------------------------------------------------- */
/* --------------------------------------------------------------------------
  Copyright (c) 2024 Ozge Kocaoglu

 THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
-------------------------------------------------------------------------- */	

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Persephone {

public class JsonViewer
{
  [System.Serializable]
  public class JsonObject 
  {
    public object jsonObject;
    public bool isOpened;

    public JsonObject(object jsonObject, bool isOpened) 
    {
      this.jsonObject = jsonObject;
      this.isOpened = isOpened;
    }
  }

  [System.Serializable]
  public class Root 
  {
    public bool isOpened;
  }

  [Serializable]
  public class JsonViewerSettings : ISerializationCallbackReceiver
  {
    public string lastJsonPath;
    public string lastJson;

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
    }
  };

  public static JsonViewerSettings jsonViewerSettings = new JsonViewerSettings();

  Dictionary<string, JsonObject> deserializedObject = new Dictionary<string, JsonObject>();

  string json;
  Root root;

  public JsonViewer() 
  {
    root = new Root();
  }

  public void setJson(string jsonString) 
  {
    json = jsonString;
    jsonParse();
  }

  void jsonParse()
  {
    if(isValidJsonObject()) {
      var objectList = json.Split(',').ToList();
      foreach (var o in objectList) {
        var objectParts = o.Split(':').ToList();

        var key = objectParts[0];
        var value = objectParts[1];
        if(!deserializedObject.ContainsKey(key)) {
          deserializedObject.Add(key, new JsonObject(value, false));
        }
      }  
    }
  }

 bool isValidJsonObject() 
 {
    Stack<string> brackets = new Stack<string>();
    var jsonCharArray = json.ToCharArray();
    for (int i = 0; i < jsonCharArray.Length; i++) {
      if (jsonCharArray[i].ToString() == "{") {
        brackets.Push(jsonCharArray[i].ToString());
      }
      else if (jsonCharArray[i].ToString() == "}") {
        if(brackets.Peek() == "{") {
          brackets.Pop();
        }
      }
    }

    return !(brackets.Count > 0);
 }

 public void draw() 
 {
    if (json == null) {
      EditorGUILayout.LabelField(new GUIContent("JSON not configured"));
    }
    else {
      root.isOpened = EditorGUILayout.Foldout(root.isOpened, "");

      if (root.isOpened) {
        foreach (var o in deserializedObject) {
          o.Value.isOpened = EditorGUILayout.Foldout(o.Value.isOpened, o.Key);
          
          if (o.Value.isOpened) {
            EditorGUILayout.SelectableLabel(o.Value.jsonObject.ToString());
          }
        }
      }
    }
 }

 public void save() 
 {
  if (!Directory.Exists("UserSettings/JsonViewerForUnity/"))
      Directory.CreateDirectory("UserSettings/JsonViewerForUnity/");
  System.IO.File.WriteAllText("UserSettings/JsonViewerForUnity/JsonViewerData", jsonViewerSettings.toJson());
 }

 public void load() 
 {
  try {
    jsonViewerSettings.fromJson(System.IO.File.ReadAllText("UserSettings/JsonViewerForUnity/JsonViewerData"));
  }
  catch(System.Exception e) {
    Debug.LogError(e);
  }
 }

}

} // End of namespace hc