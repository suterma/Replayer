using System.ComponentModel;

namespace WizardBase
{
    public class GenericChangeEventArgs<T> : CancelEventArgs
    {
        private readonly T oldValue;

        public GenericChangeEventArgs(T oldValue, T newValue) : base(false)
        {
            this.oldValue = oldValue;
            this.NewValue = newValue;
        }

        public GenericChangeEventArgs(T oldValue, T newValue, bool cancel) : base(cancel)
        {
            this.oldValue = oldValue;
            this.NewValue = newValue;
        }

        public T OldValue
        {
            get { return oldValue; }
        }

        public T NewValue { get; set; }
    }
}