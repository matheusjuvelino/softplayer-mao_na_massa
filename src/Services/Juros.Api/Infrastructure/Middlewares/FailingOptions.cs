﻿using System.Collections.Generic;

namespace Juros.Api.Infrastructure.Middlewares
{
    public class FailingOptions
    {
        public string ConfigPath = "/Failing";
        public List<string> EndpointPaths { get; set; } = new List<string>();

        public List<string> NotFilteredPaths { get; set; } = new List<string>();
    }
}
