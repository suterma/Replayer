namespace Replayer.Core.UserInteraction {
    /// <summary>
    ///     Indicates a busy state to the user
    /// </summary>
    public interface IBusyIndicator {
        /// <summary>
        ///     Determines whether this is busy, with the specified action.
        ///     The implementor should now indicate this ongoing task to the user.
        /// </summary>
        void IsBusyWith(string activityDescription);

        /// <summary>
        ///     Indicates that this is no more busy.
        /// </summary>
        void IsNoMoreBusy();
    }
}