using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RamenSea.Foundation3D.Services.KeyStore {
    public interface IKeyStoreJsonSerializer {
        public bool CanSerialize(Type t);
        public string Serialize(object obj);
        public T Deserialize<T>([NotNull] string json, [CanBeNull] T defaultValue);
    }
    public interface IKeyStoreService {
        public bool defaultAutoSave { get; set; }
        [CanBeNull] public string keyPrefix { get; set; }
        public IKeyStoreJsonSerializer jsonSerializer { get; set; }
        
        public string GetPrefixedKey(string key);
        
        public bool Contains(string key);
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