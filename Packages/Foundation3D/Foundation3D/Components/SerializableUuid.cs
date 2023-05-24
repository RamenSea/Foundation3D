using System;
using RamenSea.Foundation.Extensions;
using UnityEngine;

namespace RamenSea.Foundation3D.Components {
    /// <summary>
    /// A serializable UUID field
    /// Partially taken from Unity's solution in OpenXR and https://github.com/Searous/Unity.SerializableGuid
    /// </summary>
    [Serializable]
    public class SerializableUuid : ISerializationCallbackReceiver, IEquatable<SerializableUuid> {
        private Guid uuid;
        [SerializeField] private ulong lowBytes;
        [SerializeField] private ulong highBytes;

        public static readonly SerializableUuid Empty = new SerializableUuid(Guid.Empty);
        public SerializableUuid() {
            this.uuid = Guid.NewGuid();
            this.lowBytes = 0;
            this.highBytes = 0;
        }
        public SerializableUuid(Guid uuid) {
            this.uuid = uuid;
            this.lowBytes = 0;
            this.highBytes = 0;
        }
        public SerializableUuid(ulong lowBytes, ulong highBytes) {
            this.uuid = UUIDExtensions.CreateUuidFrom(lowBytes, highBytes);
            this.lowBytes = lowBytes;
            this.highBytes = highBytes;
        }

        public void OnAfterDeserialize() {
            uuid = UUIDExtensions.CreateUuidFrom(this.lowBytes, this.highBytes);
        }

        public void OnBeforeSerialize() {
            this.uuid.GetULong(out this.lowBytes, out this.highBytes);
        }

        public bool Equals(SerializableUuid other) => other != null && this.uuid.Equals(other.uuid);
        public override bool Equals(object obj) => obj is SerializableUuid guid && this.uuid.Equals(guid.uuid);

        public override int GetHashCode() {
            unchecked {
                var hash = this.lowBytes.GetHashCode();
                return hash * 486187739 + this.highBytes.GetHashCode();
            }
        }

        public override string ToString() => this.uuid.ToString();

        public static bool operator ==(SerializableUuid lft, SerializableUuid rgt) =>
            rgt != null && lft != null && lft.uuid == rgt.uuid;

        public static bool operator !=(SerializableUuid lft, SerializableUuid rgt) =>
            rgt != null && lft != null && lft.uuid != rgt.uuid;

        public static implicit operator SerializableUuid(Guid uuid) => new(uuid);
        public static implicit operator Guid(SerializableUuid serializable) => serializable.uuid;
    }
}