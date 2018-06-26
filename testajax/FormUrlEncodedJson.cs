using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace testajax
{
    public static class FormUrlEncodedJson
    {
        private const string ApplicationFormUrlEncoded = "application/x-www-form-urlencoded";
        private const int MinDepth = 0;

        private static readonly string[] _emptyPath = new string[1]
        {
            string.Empty
        };

        /// <summary>
        ///     Parses a collection of query string values as a <see cref="T:Newtonsoft.Json.Linq.JObject" />.
        /// </summary>
        /// <remarks>
        ///     This is a low-level API intended for use by other APIs. It has been optimized for performance and
        ///     is not intended to be called directly from user code.
        /// </remarks>
        /// <param name="nameValuePairs">
        ///     The collection of query string name-value pairs parsed in lexical order. Both names
        ///     and values must be un-escaped so that they don't contain any <see cref="T:System.Uri" /> encoding.
        /// </param>
        /// <returns>The <see cref="T:Newtonsoft.Json.Linq.JObject" /> corresponding to the given query string values.</returns>
        public static JObject Parse(IEnumerable<KeyValuePair<string, string>> nameValuePairs)
        {
            return ParseInternal(nameValuePairs, int.MaxValue, true);
        }

        /// <summary>
        ///     Parses a collection of query string values as a <see cref="T:Newtonsoft.Json.Linq.JObject" />.
        /// </summary>
        /// <remarks>
        ///     This is a low-level API intended for use by other APIs. It has been optimized for performance and
        ///     is not intended to be called directly from user code.
        /// </remarks>
        /// <param name="nameValuePairs">
        ///     The collection of query string name-value pairs parsed in lexical order. Both names
        ///     and values must be un-escaped so that they don't contain any <see cref="T:System.Uri" /> encoding.
        /// </param>
        /// <param name="maxDepth">The maximum depth of object graph encoded as <c>x-www-form-urlencoded</c>.</param>
        /// <returns>The <see cref="T:Newtonsoft.Json.Linq.JObject" /> corresponding to the given query string values.</returns>
        public static JObject Parse(IEnumerable<KeyValuePair<string, string>> nameValuePairs, int maxDepth)
        {
            return ParseInternal(nameValuePairs, maxDepth, true);
        }

        /// <summary>
        ///     Parses a collection of query string values as a <see cref="T:Newtonsoft.Json.Linq.JObject" />.
        /// </summary>
        /// <remarks>
        ///     This is a low-level API intended for use by other APIs. It has been optimized for performance and
        ///     is not intended to be called directly from user code.
        /// </remarks>
        /// <param name="nameValuePairs">
        ///     The collection of query string name-value pairs parsed in lexical order. Both names
        ///     and values must be un-escaped so that they don't contain any <see cref="T:System.Uri" /> encoding.
        /// </param>
        /// <param name="value">The parsed result or null if parsing failed.</param>
        /// <returns><c>true</c> if <paramref name="nameValuePairs" /> was parsed successfully; otherwise false.</returns>
        public static bool TryParse(IEnumerable<KeyValuePair<string, string>> nameValuePairs, out JObject value)
        {
            return (value = ParseInternal(nameValuePairs, int.MaxValue, false)) != null;
        }

        /// <summary>
        ///     Parses a collection of query string values as a <see cref="T:Newtonsoft.Json.Linq.JObject" />.
        /// </summary>
        /// <remarks>
        ///     This is a low-level API intended for use by other APIs. It has been optimized for performance and
        ///     is not intended to be called directly from user code.
        /// </remarks>
        /// <param name="nameValuePairs">
        ///     The collection of query string name-value pairs parsed in lexical order. Both names
        ///     and values must be un-escaped so that they don't contain any <see cref="T:System.Uri" /> encoding.
        /// </param>
        /// <param name="maxDepth">The maximum depth of object graph encoded as <c>x-www-form-urlencoded</c>.</param>
        /// <param name="value">The parsed result or null if parsing failed.</param>
        /// <returns><c>true</c> if <paramref name="nameValuePairs" /> was parsed successfully; otherwise false.</returns>
        public static bool TryParse(IEnumerable<KeyValuePair<string, string>> nameValuePairs, int maxDepth, out JObject value)
        {
            return (value = ParseInternal(nameValuePairs, maxDepth, false)) != null;
        }

        /// <summary>
        ///     Parses a collection of query string values as a <see cref="T:Newtonsoft.Json.Linq.JObject" />.
        /// </summary>
        /// <remarks>
        ///     This is a low-level API intended for use by other APIs. It has been optimized for performance and
        ///     is not intended to be called directly from user code.
        /// </remarks>
        /// <param name="nameValuePairs">
        ///     The collection of query string name-value pairs parsed in lexical order. Both names
        ///     and values must be un-escaped so that they don't contain any <see cref="T:System.Uri" /> encoding.
        /// </param>
        /// <param name="maxDepth">The maximum depth of object graph encoded as <c>x-www-form-urlencoded</c>.</param>
        /// <param name="throwOnError">Indicates whether to throw an exception on error or return false</param>
        /// <returns>The <see cref="T:Newtonsoft.Json.Linq.JObject" /> corresponding to the given query string values.</returns>
        private static JObject ParseInternal(IEnumerable<KeyValuePair<string, string>> nameValuePairs, int maxDepth, bool throwOnError)
        {
            if (nameValuePairs == null)
            {
                throw new ArgumentNullException(nameof(nameValuePairs));
            }
            if (maxDepth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxDepth), maxDepth, "The maximum depth of object graph encoded is out of range");
            }
            var root = new JObject();
            foreach (var nameValuePair in nameValuePairs)
            {
                var key = nameValuePair.Key;
                var str = nameValuePair.Value;
                if (key == null)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        if (throwOnError)
                        {
                            throw new ArgumentNullException(nameof(nameValuePairs));
                        }
                        return null;
                    }
                    var path = new string[1] { str };
                    if (!Insert(root, path, null, throwOnError))
                    {
                        return null;
                    }
                }
                else
                {
                    var path = GetPath(key, maxDepth, throwOnError);
                    if (path == null || !Insert(root, path, str, throwOnError))
                    {
                        return null;
                    }
                }
            }
            FixContiguousArrays(root);
            return root;
        }

        private static string[] GetPath(string key, int maxDepth, bool throwOnError)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return _emptyPath;
            }
            if (!ValidateQueryString(key, throwOnError))
            {
                return null;
            }
            var strArray = key.Split('[');
            for (var index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index].EndsWith("]", StringComparison.Ordinal))
                {
                    strArray[index] = strArray[index].Substring(0, strArray[index].Length - 1);
                }
            }
            if (strArray.Length < maxDepth)
            {
                return strArray;
            }
            if (throwOnError)
            {
                throw new Exception($"MaxDepthExceeded {maxDepth}");
            }
            return null;
        }

        private static bool ValidateQueryString(string key, bool throwOnError)
        {
            var flag = false;
            for (var index = 0; index < key.Length; ++index)
            {
                switch (key[index])
                {
                    case '[':
                        if (!flag)
                        {
                            flag = true;
                            break;
                        }
                        if (throwOnError)
                        {
                            throw new Exception($"NestedBracketNotValid application/x-www-form-urlencoded {index}");
                        }
                        return false;
                    case ']':
                        if (flag)
                        {
                            flag = false;
                            break;
                        }
                        if (throwOnError)
                        {
                            throw new Exception($"UnMatchedBracketNotValid application/x-www-form-urlencoded {index}");
                        }
                        return false;
                }
            }
            if (!flag)
            {
                return true;
            }
            if (throwOnError)
            {
                throw new Exception($"NestedBracketNotValid application/x-www-form-urlencoded {key.LastIndexOf('[')}");
            }
            return false;
        }

        private static bool Insert(JObject root, string[] path, string value, bool throwOnError)
        {
            var jobject = root;
            JObject parent = null;
            for (var i = 0; i < path.Length - 1; ++i)
            {
                if (string.IsNullOrEmpty(path[i]))
                {
                    if (throwOnError)
                    {
                        throw new Exception($"InvalidArrayInsert {BuildPathString(path, i)}");
                    }
                    return false;
                }
                if (!((IDictionary<string, JToken>)jobject).ContainsKey(path[i]))
                {
                    jobject[path[i]] = new JObject();
                }
                else if (jobject[path[i]] == null || jobject[path[i]] is JValue)
                {
                    if (throwOnError)
                    {
                        throw new Exception($"FormUrlEncodedMismatchingTypes {BuildPathString(path, i)}");
                    }
                    return false;
                }
                parent = jobject;
                jobject = jobject[path[i]] as JObject;
            }
            if (string.IsNullOrEmpty(path[path.Length - 1]) && path.Length > 1)
            {
                if (!AddToArray(parent, path, value, throwOnError))
                {
                    return false;
                }
            }
            else
            {
                if (jobject == null)
                {
                    if (throwOnError)
                    {
                        throw new Exception($"FormUrlEncodedMismatchingTypes {BuildPathString(path, path.Length - 1)}");
                    }
                    return false;
                }
                if (!AddToObject(jobject, path, value, throwOnError))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool AddToObject(JObject obj, string[] path, string value, bool throwOnError)
        {
            var i = path.Length - 1;
            var key = path[i];
            if (((IDictionary<string, JToken>)obj).ContainsKey(key))
            {
                if (obj[key] == null || obj[key].Type == JTokenType.Null)
                {
                    if (throwOnError)
                    {
                        throw new Exception($"FormUrlEncodedMismatchingTypes {BuildPathString(path, i)}");
                    }
                    return false;
                }
                if (path.Length == 1)
                {
                    if (obj[key].Type == JTokenType.String)
                    {
                        var str = obj[key].ToObject<string>();
                        var jobject = new JObject();
                        jobject.Add("0", str);
                        jobject.Add("1", value);
                        obj[key] = jobject;
                    }
                    else if (obj[key] is JObject)
                    {
                        var jsonObject = obj[key] as JObject;
                        var index = GetIndex(jsonObject, throwOnError);
                        if (index == null)
                        {
                            return false;
                        }
                        jsonObject.Add(index, value);
                    }
                }
                else
                {
                    if (throwOnError)
                    {
                        throw new Exception($"JQuery13CompatModeNotSupportNestedJson {BuildPathString(path, i)}");
                    }
                    return false;
                }
            }
            else
            {
                obj[key] = value != null ? value : null;
            }
            return true;
        }

        private static bool AddToArray(JObject parent, string[] path, string value, bool throwOnError)
        {
            var index1 = path[path.Length - 2];
            var jsonObject = parent[index1] as JObject;
            if (jsonObject == null)
            {
                if (throwOnError)
                {
                    throw new Exception($"FormUrlEncodedMismatchingTypes {BuildPathString(path, path.Length - 1)}");
                }
                return false;
            }
            var index2 = GetIndex(jsonObject, throwOnError);
            if (index2 == null)
            {
                return false;
            }
            jsonObject.Add(index2, value);
            return true;
        }

        private static string GetIndex(JObject jsonObject, bool throwOnError)
        {
            var num = -1;
            if (jsonObject.Count > 0)
            {
                foreach (var key in ((IDictionary<string, JToken>)jsonObject).Keys)
                {
                    int result;
                    if (int.TryParse(key, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result) && result > num)
                    {
                        num = result;
                    }
                    else
                    {
                        if (throwOnError)
                        {
                            throw new Exception($"FormUrlEncodedMismatchingTypes {key}");
                        }
                        return null;
                    }
                }
            }
            return (num + 1).ToString(CultureInfo.InvariantCulture);
        }

        private static void FixContiguousArrays(JToken jv)
        {
            var jarray = jv as JArray;
            if (jarray != null)
            {
                for (var index = 0; index < jarray.Count; ++index)
                {
                    if (jarray[index] != null)
                    {
                        jarray[index] = FixSingleContiguousArray(jarray[index]);
                        FixContiguousArrays(jarray[index]);
                    }
                }
            }
            else
            {
                var jobject = jv as JObject;
                if (jobject == null || jobject.Count <= 0)
                {
                    return;
                }
                foreach (var index in new List<string>(((IDictionary<string, JToken>)jobject).Keys))
                {
                    if (jobject[index] != null)
                    {
                        jobject[index] = FixSingleContiguousArray(jobject[index]);
                        FixContiguousArrays(jobject[index]);
                    }
                }
            }
        }

        private static JToken FixSingleContiguousArray(JToken original)
        {
            var jobject = original as JObject;
            List<string> sortedKeys;
            if (jobject == null || jobject.Count <= 0 || !CanBecomeArray(new List<string>(((IDictionary<string, JToken>)jobject).Keys), out sortedKeys))
            {
                return original;
            }
            var jarray = new JArray();
            foreach (var index in sortedKeys)
            {
                jarray.Add(jobject[index]);
            }
            return jarray;
        }

        private static bool CanBecomeArray(List<string> keys, out List<string> sortedKeys)
        {
            var source = new List<ArrayCandidate>();
            sortedKeys = null;
            var flag = true;
            foreach (var key in keys)
            {
                int result;
                if (!int.TryParse(key, NumberStyles.None, CultureInfo.InvariantCulture, out result))
                {
                    flag = false;
                    break;
                }
                var str = result.ToString(CultureInfo.InvariantCulture);
                if (!str.Equals(key, StringComparison.Ordinal))
                {
                    flag = false;
                    break;
                }
                source.Add(new ArrayCandidate(result, str));
            }
            if (flag)
            {
                source.Sort((x, y) => x.Key - y.Key);
                for (var index = 0; index < source.Count; ++index)
                {
                    if (source[index].Key != index)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag)
            {
                sortedKeys = new List<string>(source.Select(x => x.Value));
            }
            return flag;
        }

        private static string BuildPathString(string[] path, int i)
        {
            var stringBuilder = new StringBuilder(path[0]);
            for (var index = 1; index <= i; ++index)
            {
                stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[{0}]", path[index]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>Class that wraps key-value pairs.</summary>
        /// <remarks>
        ///     This use of this class avoids a FxCop warning CA908 which happens if using various generic types.
        /// </remarks>
        private class ArrayCandidate
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="T:System.Net.Http.Formatting.FormUrlEncodedJson.ArrayCandidate" />
            ///     class.
            /// </summary>
            /// <param name="key">
            ///     The key of this <see cref="T:System.Net.Http.Formatting.FormUrlEncodedJson.ArrayCandidate" />
            ///     instance.
            /// </param>
            /// <param name="value">
            ///     The value of this <see cref="T:System.Net.Http.Formatting.FormUrlEncodedJson.ArrayCandidate" />
            ///     instance.
            /// </param>
            public ArrayCandidate(int key, string value)
            {
                Key = key;
                Value = value;
            }

            /// <summary>
            ///     Gets or sets the key of this <see cref="T:System.Net.Http.Formatting.FormUrlEncodedJson.ArrayCandidate" />
            ///     instance.
            /// </summary>
            /// <value>
            ///     The key of this <see cref="T:System.Net.Http.Formatting.FormUrlEncodedJson.ArrayCandidate" /> instance.
            /// </value>
            public int Key { get; }

            /// <summary>
            ///     Gets or sets the value of this <see cref="T:System.Net.Http.Formatting.FormUrlEncodedJson.ArrayCandidate" />
            ///     instance.
            /// </summary>
            /// <value>
            ///     The value of this <see cref="T:System.Net.Http.Formatting.FormUrlEncodedJson.ArrayCandidate" /> instance.
            /// </value>
            public string Value { get; }
        }
    }
}
