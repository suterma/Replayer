using System;
using System.ComponentModel;
using Replayer.Model;

namespace Replayer.WinForms.Ui.Views.Tracks {
    internal class DisplayCompilation {
        [Browsable(false)]
        public ICompilation Model { get; set; }

        [Browsable(true)]
        public String Title {
            get { return Model.Title; }
            set { Model.Title = value; }
        }

        /// <summary>
        ///     Create a new instance
        /// </summary>
        /// <param name="model"></param>
        internal DisplayCompilation(ICompilation model) {
            Model = model;
        }
    }
}