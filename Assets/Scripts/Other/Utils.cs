/* --------------------------------------------------------------------------
    Title       :  Utils.cs
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
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Persephone {

public static class Utils
{
    public static string toJson(this object obj, bool prettyPrint = true) 
    {
        return JsonUtility.ToJson(obj, prettyPrint);
    }

    public static void fromJson(this object obj, string json)
    {
        JsonUtility.FromJsonOverwrite(json, obj); 
    }
}
}