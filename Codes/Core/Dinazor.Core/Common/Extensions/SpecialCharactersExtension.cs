using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinazor.Core.Database.Helper
{
    /// <summary>
    ///     We can't really expect the user to write '\:' now can we?!
    /// </summary>
    internal static class SpecialCharactersExtension
    {
        #region Static Fields

        /// <summary>
        ///     The specials.
        /// </summary>
        private static readonly string[] Specials = { ":" };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Escapes special characters such as ':' so they can be included in the search strings
        /// </summary>
        /// <param name="text">
        ///     The text that contains special characters that need escaping
        /// </param>
        /// <returns>
        ///     The text with special characters escaped
        /// </returns>
        public static string Escape(this string text)
        {
            return Specials.Aggregate(text, (current, special) => current.Replace(special, "\\" + special));
        }

        #endregion
    }
}
