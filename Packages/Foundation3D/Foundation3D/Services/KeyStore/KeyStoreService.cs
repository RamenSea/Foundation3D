using System;
using RamenSea.Foundation.Extensions;
using RamenSea.Foundation.General;
using UnityEngine;

namespace RamenSea.Foundation3D.Services.KeyStore {
    /// <summary>
    /// The generic implementation of IKeyStoreService built on top of Unity's PlayerPrefs
    /// This is a very useful tool for small projects or for saving a small amount of data.
    ///
    /// This should not be used if you plan on saving large sets of data. Foundation does not offer a solution for that
    /// However there are several good solutions that exist in the Unity ecosystem.
    /// 
    /// TODO: incorporate a better system for large save files and large json blobs
    /// </summary>
    public class KeyStoreService : IKeyStoreService {
        public bool defaultAutoSave { get; set; } = true;
        public string keyPrefix { get; set; } = null;
        public IKeyStoreJsonSerializer jsonSerializer { get; set; } = null;

        public string GetPrefixedKey(string key) {
            if (this.keyPrefix == null) {
                return key;
            }

            return this.keyPrefix + key;
        }

        public bool Contains(string key) => PlayerPrefs.HasKey(this.GetPrefixedKey(key));

        public bool GetBool(string key, bool defaultValue = false) {
            var i = this.GetInt(key, -1);

            if (i < 0 || i > 1) {
                return defaultValue;
            }

            return i == 1;
        }

        public int GetInt(string key, int defaultValue = 0) =>
            PlayerPrefs.GetInt(this.GetPrefixedKey(key), defaultValue);

        public float GetFloat(string key, float defaultValue = 0) =>
            PlayerPrefs.GetFloat(this.GetPrefixedKey(key), defaultValue);
        
        public long GetLong(string key, long defaultValue = 0) {
            var s = this.GetString(key);
            return s.ToLong(defaultValue);
        }
        public double GetDouble(string key, double defaultValue = 0) {
            var s = this.GetString(key);
            return s.ToDouble(defaultValue);
        }

        public T GetDeserializedObject<T>(string key, T defaultValue = default) {
            if (this.jsonSerializer == null || this.jsonSerializer.CanSerialize(typeof(T)) == false) {
                return defaultValue;
            }

            var jsonString = this.GetString(key);
            if (string.IsNullOrEmpty(jsonString)) {
                return defaultValue;
            }

            return this.jsonSerializer.Deserialize<T>(jsonString, defaultValue);
        }

        public Guid GetUuid(string key, Guid defaultValue = default) {
            var uuidString = this.GetString(key);
            if (string.IsNullOrEmpty(uuidString)) {
                return defaultValue;
            }
            return Guid.Parse(uuidString);
        }
        public string GetString(string key, string defaultValue = null) => PlayerPrefs.GetString(this.GetPrefixedKey(key), defaultValue);
        public void Set(string key, object obj) => this.Set(key, obj, this.defaultAutoSave);
        public void Set(string key, object obj, bool autoSave) {
            if (obj == null) {
                PlayerPrefs.DeleteKey(key);
            } else if (obj is bool b) {
                PlayerPrefs.SetInt(key, b ? 1 : 0);
            } else if (obj is int i) {
                PlayerPrefs.SetInt(key, i);
            } else if (obj is long l) {
                PlayerPrefs.SetString(key, l.ToString());
            } else if (obj is float f) {
                PlayerPrefs.SetFloat(key, f);
            } else if (obj is double d) {
                PlayerPrefs.SetString(key, d.ToString());
            } else if (obj is string s) {
                PlayerPrefs.SetString(key, s);
            } else if (obj is Guid uuid) {
                PlayerPrefs.SetString(key, uuid.ToString());
            } else if (this.jsonSerializer != null && this.jsonSerializer.CanSerialize(obj.GetType())) {
                PlayerPrefs.SetString(key, this.jsonSerializer.Serialize(obj));
            } else {
                throw new BaseFoundationException("Unsupported type");
            }

            if (autoSave) {
                this.Save();
            }
        }

        public void Remove(string key) => PlayerPrefs.DeleteKey(this.GetPrefixedKey(key));
        public void Save() => PlayerPrefs.Save();
        
        public void DeleteAll() => PlayerPrefs.DeleteAll();
    }
}