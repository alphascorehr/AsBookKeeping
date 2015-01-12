using System;
using Csla;
using Csla.Serialization;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BusinessObjects.CoreBusinessClasses
{
    [Serializable]
    public abstract class CoreBusinessChildClass<T> : BusinessBase<T> where T : CoreBusinessChildClass<T>
    {
        public static readonly PropertyInfo<byte[]> EntityKeyDataProperty = RegisterProperty<byte[]>(c => c.EntityKeyData);
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] EntityKeyData
        {
            get { return GetProperty(EntityKeyDataProperty); }
            set { SetProperty(EntityKeyDataProperty, value); }
        }

        protected static byte[] Serialize(object obj)
        {
            using (var buffer = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(buffer, obj);
                return buffer.ToArray();
            }
        }

        protected static object Deserialize(byte[] data)
        {
            using (var buffer = new MemoryStream(data))
            {
                var formatter = new BinaryFormatter();
                return formatter.Deserialize(buffer);
            }
        }

        protected CoreBusinessChildClass()
            : base() { }
    }
}
