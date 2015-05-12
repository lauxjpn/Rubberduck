﻿namespace Rubberduck.VBEditor
{
    public interface IActiveCodePaneEditor
    {
        /// <summary>
        /// Gets entire lines in specified <see cref="selection"/>.
        /// </summary>
        /// <param name="selection">Specifies the <c>StartLine</c> and <c>LineCount</c> to return.</param>
        string GetLines(Selection selection);

        /// <summary>
        /// Deletes entire lines in specified <see cref="selection"/>.
        /// </summary>
        /// <param name="selection">Specifies the <c>StartLine</c> and <c>LineCount</c> to delete.</param>
        void DeleteLines(Selection selection);

        /// <summary>
        /// Replaces an entire line with specified <see cref="content"/>.
        /// </summary>
        /// <param name="line">The line number to replace.</param>
        /// <param name="content">The new content.</param>
        void ReplaceLine(int line, string content);

        /// <summary>
        /// Inserts specified content at specified line.
        /// </summary>
        /// <param name="line">The line number to insert content at.</param>
        /// <param name="content">The content to insert.</param>
        void InsertLines(int line, string content);
    }
}
