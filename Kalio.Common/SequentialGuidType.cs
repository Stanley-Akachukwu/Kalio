﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalio.Common
{
    public enum SequentialGuidType
    {
        /// <summary>
        /// The GUID should be sequential when formatted using the
        /// <see cref="Guid.ToString()" /> method.
        /// </summary>
        SequentialAsString,

        /// <summary>
        /// The GUID should be sequential when formatted using the
        /// <see cref="Guid.ToByteArray" /> method.
        /// </summary>
        SequentialAsBinary,

        /// <summary>
        /// The sequential portion of the GUID should be located at the end
        /// of the Data4 block.
        /// </summary>
        SequentialAtEnd,

        SequentialAsUlid,
        Timestamp
    }
}
