﻿
namespace Logrila.Logging
{
    public interface ILogger
    {
        ILog Get(string name);
    }
}
