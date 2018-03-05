namespace Replayer.Core.UserInteraction {
    /// <summary>
    ///     A faked busy indicator that just does nothing. It is used to initialize
    ///     the model with an existing player, as long as no
    ///     real player is set.
    /// </summary>
    internal class FakeBusyIndicator : IBusyIndicator {
        /// <summary>
        ///     Determines whether this is busy, with the specified action.
        ///     The implementor should now indicate this ongoing task to the user.
        /// </summary>
        /// <param name="activityDescription"></param>
        public void IsBusyWith(string activityDescription) {}

        /// <summary>
        ///     Indicates that this is no more busy.
        /// </summary>
        public void IsNoMoreBusy() {}
    }
}