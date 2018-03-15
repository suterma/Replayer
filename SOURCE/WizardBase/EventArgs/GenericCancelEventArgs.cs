using System.ComponentModel;

namespace WizardBase {
    public delegate void GenericCancelEventHandler<T>(object sender, GenericCancelEventArgs<T> tArgs);

    public class GenericCancelEventArgs<T> : CancelEventArgs {
        public GenericCancelEventArgs(T value) : base(false) {
            this.Value = value;
        }

        public GenericCancelEventArgs(T value, bool cancel) : base(cancel) {
            this.Value = value;
        }

        public T Value { get; set; }
    }
}