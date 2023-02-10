﻿using System;

namespace ShardingCore.Test2x.Domain.Entities
{
    public class LogDay
    {
        public Guid Id { get; set; }
        public string LogLevel { get; set; }
        public string LogBody { get; set; }
        public DateTime LogTime { get; set; }
    }
}
