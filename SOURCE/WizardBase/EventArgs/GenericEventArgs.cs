using System;

namespace WizardBase
{
    public delegate void GenericEventHandler<T>(object sender, GenericEventArgs<T> tArgs);

    public class GenericEventArgs<T> : EventArgs
    {
        public GenericEventArgs()
        {
            Value = default(T);
        }

        public GenericEventArgs(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
    }
}