using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSerialization {

	public class PRSerializer {

		public byte[] Serialize(object toSerialize) {

			if(toSerialize == null)
				return new byte[0];

			return BitConverter.GetBytes(toSerialize.GetType().GetHashCode());
		}

		public T Deserialize<T>(byte[] serializedObject) {

			if(serializedObject.Length == 0)
				return default(T);

			return (T)Activator.CreateInstance(typeof(T));
		}
	}
}
