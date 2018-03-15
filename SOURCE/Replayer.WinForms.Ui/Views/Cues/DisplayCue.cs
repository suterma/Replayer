using System;
using System.Collections.Generic;
using System.Diagnostics;
using Replayer.Model;

namespace Replayer.WinForms.Ui.Views.Cues {
    [DebuggerDisplay("\\{ Description = {Description}, Item = {Item} \\}")]
    public sealed class DisplayCue : IEquatable<DisplayCue> {
        private readonly string _Description;
        private readonly Cue _Item;

        public string Description {
            get { return _Description; }
        }

        public Cue Item {
            get { return _Item; }
        }

        public DisplayCue(string description, Cue item) {
            _Description = description;
            _Item = item;
        }

        public bool Equals(DisplayCue obj) {
            if (obj == null) {
                return false;
            }
            if (!EqualityComparer<string>.Default.Equals(_Description, obj._Description)) {
                return false;
            }
            if (!EqualityComparer<Cue>.Default.Equals(_Item, obj._Item)) {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj) {
            if (obj is DisplayCue) {
                return Equals((DisplayCue)obj);
            }
            return false;
        }

        public override int GetHashCode() {
            int hash = 0;
            hash ^= EqualityComparer<string>.Default.GetHashCode(_Description);
            hash ^= EqualityComparer<Cue>.Default.GetHashCode(_Item);
            return hash;
        }

        public override string ToString() {
            return Description;
            //return String.Format("{{ Description = {0}, Item = {1} }}", _Description, _Item);
        }
    }
}