using System;
using NaughtyAttributes;
using RamenSea.Foundation3D.Components;
using UnityEngine;

namespace Tests.UuidTests {
    public class TestUuidSerialization: MonoBehaviour {
        public SerializableUuid testUuidField;


        [Button("Test UUID")]
        public void TestUuid() {
            Debug.Log(this.testUuidField.ToString());
            Debug.Log(((Guid) this.testUuidField).ToByteArray());
        }
    }
}