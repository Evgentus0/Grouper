﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.DTOs.Outbound
{
    public class ErrorModel
    {
        public List<string> Fields { get; set; }
        /// <summary>
        /// A URI reference [RFC3986] that identifies the
        ///problem type.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// A short, human-readable summary of the problem type.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem.
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// The HTTP status code ([RFC7231], Section 6)
        ///generated by the origin server for this occurrence of the problem.
        /// </summary>
        public int Status { get; set; }
    }
}