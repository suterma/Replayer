using System;
using Replayer.Core.Data;

namespace Replayer.Core {
    /// <summary>
    ///     A broker of events of the Replayer application.
    /// </summary>
    /// <remarks>This serves as an exchange for all events within the application.</remarks>
    public sealed class EventBroker {
        /// <summary>
        ///     The broker
        /// </summary>
        private static readonly EventBroker instance = new EventBroker();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit

        /// <summary>
        ///     Gets the instance of the EventBroker.
        /// </summary>
        /// <value>The instance.</value>
        public static EventBroker Instance {
            get { return instance; }
        }

        static EventBroker() {}


        /// <summary>
        ///     Occurs when [event occured].
        /// </summary>
        public event EventHandler<EventArgs<String>> EventOccured;

        /// <summary>
        ///     Called when [event occured].
        /// </summary>
        private void OnEventOccured(String occuredEvent) {
            if (EventOccured != null) //anyone listening?
            {
                EventOccured(this, new EventArgs<String> {Data = occuredEvent});
            }
        }

        /// <summary>
        ///     Issues the event.
        /// </summary>
        /// <param name="occuredEvent">The occured event.</param>
        public void IssueEvent(String occuredEvent) {
            OnEventOccured(occuredEvent);
        }
    }
}