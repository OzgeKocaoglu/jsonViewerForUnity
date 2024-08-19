/* --------------------------------------------------------------------------
  Title       :  JsonViewerEditor.cs
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

using System.Linq;
using System.Text;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Persephone {


public class JsonViewerEditor : EditorWindow
{
  static JsonViewerEditor jsonWindow;
  
  [UnityEditor.MenuItem("Persephone/General/JsonViewer")]
  static void init() 
  {
    jsonWindow = (JsonViewerEditor)EditorWindow.GetWindow(typeof(JsonViewerEditor), false);
    jsonWindow.Show(true);
  }

  JsonViewer jsonViewer;
  string currentText;
  string lastPath;

  void OnEnable() 
  {
    jsonViewer = new JsonViewer();
    jsonViewer.load();
    currentText = JsonViewer.jsonViewerSettings.lastJson;
    lastPath = JsonViewer.jsonViewerSettings.lastJsonPath;
  }

  void OnDisable() 
  {
    jsonViewer.save();
  }

  void OnGUI()
  {
    EditorGUILayout.BeginVertical();
    if (String.IsNullOrWhiteSpace(currentText)) {
      EditorGUILayout.LabelField(new GUIContent("Json is empty."));
    }
    else {
      EditorGUILayout.TextField(lastPath);
    }

    EditorGUILayout.EndVertical();

    if (GUILayout.Button("Select Json")) {
      lastPath = EditorUtility.OpenFilePanel("Open json file", "", "json");
      JsonViewer.jsonViewerSettings.lastJsonPath = lastPath;
      if (lastPath.Length != 0) {
        currentText = File.ReadAllText(lastPath);
        JsonViewer.jsonViewerSettings.lastJson = currentText;
        jsonViewer.setJson(currentText);
      }
    }


    if (jsonViewer == null) return;
    jsonViewer.draw();
  }


}

} // End of namespace hc