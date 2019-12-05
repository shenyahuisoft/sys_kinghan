﻿using System;
using System.Text;

namespace Logrila.Logging
{
    public static class Logger
    {
        private static ILogger _logger;

        public static ILogger Current
        {
            get { return _logger ?? (_logger = new TraceLogger()); }
        }

        public static ILog Get<T>()
            where T : class
        {
            return Get(GetCleanTypeName<T>());
        }

        public static ILog Get(Type type)
        {
            return Get(GetCleanTypeName(type));
        }

        public static ILog Get(string name)
        {
            return Current.Get(name);
        }

        public static void UseLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static string GetCleanTypeName<T>()
        {
            return GetCleanTypeName(typeof(T));
        }

        public static string GetCleanTypeName(Type type)
        {
            return GetCleanTypeName(new StringBuilder(), type, null);
        }

        private static string GetCleanTypeName(StringBuilder sb, Type type, string scope)
        {
            if (type.IsGenericParameter)
                return "";

            if (type.Namespace != null)
            {
                string ns = type.Namespace;
                if (!ns.Equals(scope))
                {
                    sb.Append(ns);
                    sb.Append(".");
                }
            }

            if (type.IsNested)
            {
                GetCleanTypeName(sb, type.DeclaringType, type.Namespace);
                sb.Append("+");
            }

            if (type.IsGenericType)
            {
                string name = type.GetGenericTypeDefinition().Name;

                int index = name.IndexOf('`');
                if (index > 0)
                    name = name.Remove(index);

                sb.Append(name);
                sb.Append("<");

                Type[] arguments = type.GetGenericArguments();
                for (int i = 0; i < arguments.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }

                    GetCleanTypeName(sb, arguments[i], type.Namespace);
                }

                sb.Append(">");
            }
            else
                sb.Append(type.Name);

            return sb.ToString();
        }
    }
}
