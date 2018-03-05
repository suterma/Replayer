using System;
using System.IO;
using Replayer.Model.Persistence;

namespace Replayer.Model {
    /// <summary>
    ///     The supported compilation types.
    /// </summary>
    public enum CompilationType {
        Zip,
        Xml,
        Quirli,
    }

    /// <summary>
    ///     A factory for Compilation handling.
    /// </summary>
    public static class CompilationFactory {
        /// <summary>
        ///     Converts the specified Compilation to the specified target type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Compilation">The Compilation.</param>
        /// <returns></returns>
        public static ICompilation Convert(ICompilation source, CompilationType targetType) {
            ICompilation converted;
            switch (targetType) {
                case CompilationType.Zip:
                    converted = new ZipCompilation();
                    break;
                case CompilationType.Xml:
                    converted = new XmlCompilation();
                    break;
                case CompilationType.Quirli:
                    converted = new QuirliCompilation();
                    break;
                default:
                    throw new NotSupportedException();
                    break;
            }
            converted.Url = source.Url;
            converted.MediaPath = source.MediaPath;
            converted.Title = source.Title;
            converted.Tracks = source.Tracks;
            return converted;
        }

        /// <summary>
        ///     Retrieves the Compilation at the specified url.
        /// </summary>
        public static ICompilation Retrieve(string url) {
            //load initial data
            if (Path.GetExtension(url).Equals(XmlCompilation.DefaultExtension)) //is xml Compilation?
            {
                return new XmlCompilation().Retrieve(url);
            } else if (Path.GetExtension(url).Equals(ZipCompilation.DefaultExtension)) //is zipped Compilation?
            {
                return new ZipCompilation().Retrieve(url);
            } else if (Path.GetExtension(url).Equals(QuirliCompilation.DefaultExtension)) //is Quirli Compilation?
            {
                return new QuirliCompilation().Retrieve(url);
            }
            throw new NotSupportedException("The specified URL does not appear to point to a valid compilation.");
        }


        /// <summary>
        ///     Creates a new compilation of default type, which is Xml.
        /// </summary>
        /// <returns></returns>
        public static ICompilation CreateNew() {
            return new XmlCompilation();
        }
    }
}