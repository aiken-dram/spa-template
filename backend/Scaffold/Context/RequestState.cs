﻿using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class RequestState
    {
        public RequestState()
        {
            Requests = new HashSet<Request>();
        }

        public string? Desc { get; set; }
        public int IdState { get; set; }
        public string State { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
