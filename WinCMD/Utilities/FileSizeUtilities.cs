using System;
using System.IO;

namespace WinCMD.Utilities
{
    /// <summary>The <see cref="FileSizeUtilities" /> class.</summary>
    public class FileSizeUtilities
    {
        /// <summary>
        ///     The <see cref="NameSize" /> enum.
        /// </summary>
        public enum NameSize
        {
            /// <summary>
            ///     The short name.
            /// </summary>
            Short,

            /// <summary>
            ///     The full name.
            /// </summary>
            Full
        }

        /// <summary>
        ///     The file split byte size.
        /// </summary>
        public const int FileSplit = 1024;

        /// <summary>
        ///     The short names.
        /// </summary>
        public static string[] ShortNames = {"B", "KB", "MB", "GB", "TB", "PB", "EB, ZB, YB"};

        /// <summary>
        ///     The long names.
        /// </summary>
        public static string[] LongNames =
            {"Byte", "Kilobyte", "Megabyte", "Gigabyte", "Terabyte", "Petabyte", "Exabyte", "Zettabyte", "Yottabyte"};

        /// <summary>
        ///     Gets the readable size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="nameSize">The name size.</param>
        /// <returns>The readable string size.</returns>
        public static string GetReadableSize(long size, NameSize nameSize = NameSize.Short)
        {
            string[] sizes;

            switch (nameSize)
            {
                case NameSize.Short:
                    sizes = ShortNames;
                    break;
                case NameSize.Full:
                    sizes = LongNames;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameSize), nameSize, null);
            }

            double length = size;
            int order = 0;

            while (length >= FileSplit && order < sizes.Length - 1)
            {
                order++;
                length /= FileSplit;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            string result = $"{length:0.##} {sizes[order]}";
            return result;
        }

        /// <summary>
        ///     Gets the size of the readable file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="nameSize">The name size.</param>
        /// <returns>The readable file size.</returns>
        public static string GetReadableFileSize(string path, NameSize nameSize = NameSize.Short)
        {
            long length = new FileInfo(path).Length;

            return GetReadableSize(length, nameSize);
        }
    }
}