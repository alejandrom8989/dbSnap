// -----------------------------------------------------------------------
// <copyright file="ISnapshot.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Domain.DomainInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISnapshot
    {
        string Name { get; }

        string SourceDatabase { get; }
    }
}
