using NUnit.Framework;
using PRSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSerializationTests {

	[TestFixture]
	class SerializerTests {

		PRSerializer serializer;

		[SetUp]
		public void Setup() {

			serializer = new PRSerializer();
		}

		[Test]
		public void CreateSerializableObject() {

			SerializeTest1 objectToSerialize = new SerializeTest1();
		}

		[Test]
		public void GivenNullObject_ReturnEmptyByteArray() {

			byte[] bytes = serializer.Serialize(null);

			Assert.AreEqual(bytes, new byte[0]);
		}

		[Test]
		public void GivenEmptyByteArray_ReturnNullObject() {

			byte[] input = new byte[0];

			Assert.IsNull(serializer.Deserialize<SerializeTest1>(input));
		}

		[Test]
		public void GivenEmptyObject_SerializeClassTypeOnly() {

			byte[] sutOut = GivenSerializedEmptyObject();
			AssertEqualSerializedEmptyObjects(sutOut);
		}

		[Test]
		public void GivenSerializedEmptyObject_ReturnTypifiedEmptyObject() {

			byte[] sutOut = GivenSerializedEmptyObject();
			AssertEmptyClassDeserialized(sutOut);
		}

		[Test]
		public void GivenIntegerObject_SerializeObjectWith4ByteBody() {

			byte[] sutOut = GivenSerializedIntegerObject();
			AssertEqualSerializedObjectsWithIntegerField(sutOut);
		}

		private void AssertEmptyClassDeserialized(byte[] sutOut) {

			Assert.AreNotEqual(sutOut.Length, 0);
			Assert.AreEqual(serializer.Deserialize<SerializeTest1>(sutOut).GetType(), typeof(SerializeTest1));
		}

		private void AssertEqualSerializedEmptyObjects(byte[] sutOut) {

			SerializeTest1 obj = new SerializeTest1();
			int hash = obj.GetType().GetHashCode();

			byte[] testOutput = BitConverter.GetBytes(hash);

			Assert.AreEqual(testOutput, sutOut);
		}

		private void AssertEqualSerializedObjectsWithIntegerField(byte[] sutOut) {

			SerializeTest2 obj = new SerializeTest2();

			byte[] def = BitConverter.GetBytes(obj.GetType().GetHashCode());
			byte[] payload = BitConverter.GetBytes(obj.testInt);

			List<byte> retList = new List<byte>();
			retList.AddRange(def);
			retList.AddRange(payload);

			Assert.AreEqual(sutOut, retList.ToArray());
		}

		private byte[] GivenSerializedEmptyObject() {

			SerializeTest1 obj = new SerializeTest1();
			return serializer.Serialize(obj);
		}

		private byte[] GivenSerializedIntegerObject() {

			SerializeTest2 obj = new SerializeTest2();
			return serializer.Serialize(obj);
		}

		private byte[] GivenSerializedObjectWithIntegerField() {

			return new byte[0];
		}
	}

	[PRSerializable]
	class SerializeTest1 {

	}

	[PRSerializable]
	class SerializeTest2 {

		public int testInt = 5;
	}
}


