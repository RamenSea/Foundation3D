using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace RamenSea.Foundation3D.Services.KeyStore {
    /// <summary>
    /// JSON Serializer that can work with IKeyStoreService
    /// </summary>
    public interface IKeyStoreJsonSerializer {
        public bool CanSerialize(Type t);
        public string Serialize(object obj, bool prettyPrint = false);
        public T Deserialize<T>([NotNull] string json, [CanBeNull] T defaultValue);
    }

    /// <summary>
    /// Implementation of IKeyStoreJsonSerializer that works on Unity's own JSON system
    /// </summary>
    public struct UnityJsonSerializer: IKeyStoreJsonSerializer {
        public bool CanSerialize(Type t) {
            return true; //todo Unity JSON
        }
        public string Serialize(object obj, bool prettyPrint = false) {
            return JsonUtility.ToJson(obj, prettyPrint: prettyPrint);
        }
        public T Deserialize<T>(string json, T defaultValue) {
            var t = JsonUtility.FromJson<T>(json);
            if (t == null) {
                return defaultValue;
            }

            return t;
        }
    }
    /// <summary>
    /// An interface for a key store service. Keeping this class generic will allow for different key store solutions
    /// to be used at different stages of development or the life cycle of your product.
    ///
    /// Supported primitives for the KeyStoreService implementation is:
    /// - bool - stored as an int
    /// - int - stored as an int
    /// - long - stored as a string
    /// - float - stored as an float
    /// - double - stored as a string
    /// - Guid (UUID, int128) - stored as a string
    /// - string - stored as a string
    ///
    /// </summary>
    public interface IKeyStoreService {
        /// <summary>
        /// Whether or not IKeyStoreService will save after each edit
        /// </summary>
        public bool defaultAutoSave { get; set; }
        /// <summary>
        /// Used as a prefix for each key. This is useful if you want to support multi-users in your game.
        /// For example User-A and User-B both play your game and each have their own accounts.
        /// You can simple set the "keyPrefix" to be "user_a" and it will prepend each key used with "user_a".
        /// </summary>
        [CanBeNull] public string keyPrefix { get; set; }
        public IKeyStoreJsonSerializer jsonSerializer { get; set; }
        
        /// <summary>
        /// Returns a properly prefixed key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetPrefixedKey(string key);
        
        /// <summary>
        /// Checks whether or not a key is present
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key);
        /// <summary>
        /// Set the object to the key store with the key.
        /// Not all primitives are supported, please see the list up top.
        ///
        /// If a null obj is passed in, the key will be removed from the keystore
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="autoSave"></param>
        public void Set([NotNull] string key, [CanBeNull] object obj, bool autoSave);
        public void Set([NotNull] string key, [CanBeNull] object obj);
        
        // Primitives
        public bool GetBool([NotNull] string key, bool defaultValue = false);
        
        public int GetInt([NotNull] string key, int defaultValue = 0);
        public long GetLong([NotNull] string key, long defaultValue = 0);
        public float GetFloat([NotNull] string key, float defaultValue = 0);
        public double GetDouble([NotNull] string key, double defaultValue = 0);
        
        [CanBeNull]
        public T GetDeserializedObject<T>([NotNull] string key, T defaultValue = default);
        
        public Guid GetUuid([NotNull] string key, Guid defaultValue = default);

        [CanBeNull]
        public string GetString([NotNull] string key, [CanBeNull] string defaultValue = null);

        public void Remove(string key);
        public void Save();
        public void DeleteAll();
    }
}