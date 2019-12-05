using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace CommonUtil
{

    public class ClassReflection
    {
        // Fields
        private object m_Instance;
        private Type m_type;

        // Methods
        public ClassReflection(object obj)
        {
            this.m_type = null;
            this.m_Instance = null;
            try
            {
                this.m_type = obj.GetType();
                this.m_Instance = obj;
            }
            catch (Exception exception)
            {
                throw new CommonException("构造类失败", exception);
            }
        }

        public ClassReflection(string className)
            : this(className, null)
        {
        }

        public ClassReflection(Type className)
            : this(className, null)
        {
        }

        public ClassReflection(string className, object[] objs)
        {
            this.m_type = null;
            this.m_Instance = null;
            if (!className.Equals(""))
            {
                try
                {
                    this.m_type = LoadType(className);
                    if (objs == null)
                    {
                        this.m_Instance = this.m_type.Assembly.CreateInstance(className);
                    }
                    else
                    {
                        this.m_Instance = this.m_type.Assembly.CreateInstance(className, true, BindingFlags.CreateInstance, null, objs, CultureInfo.CurrentCulture, null);
                    }
                }
                catch (Exception exception)
                {
                    throw new CommonException("构造类失败", exception);
                }
            }
        }

        public ClassReflection(Type className, object[] objs)
        {
            this.m_type = null;
            this.m_Instance = null;
            if (className != null)
            {
                this.m_type = className;
                try
                {
                    if (objs == null)
                    {
                        this.m_Instance = this.m_type.Assembly.CreateInstance(className.FullName);
                    }
                    else
                    {
                        this.m_Instance = this.m_type.Assembly.CreateInstance(className.FullName, true, BindingFlags.CreateInstance, null, objs, CultureInfo.CurrentCulture, null);
                    }
                }
                catch (Exception exception)
                {
                    throw new CommonException("构造类失败", exception);
                }
            }
        }

        public static object ConvertTo(object Value, Type type)
        {
            object obj2;
            try
            {
                if (type.FullName == "System.Int16")
                {
                    return Convert.ToInt16(Value);
                }
                if (type.FullName == "System.Int32")
                {
                    return Convert.ToInt32(Value);
                }
                if (type.FullName == "System.Int64")
                {
                    return Convert.ToInt64(Value);
                }
                if (type.FullName == "System.String")
                {
                    return Convert.ToString(Value);
                }
                if (type.FullName == "System.DateTime")
                {
                    return Convert.ToDateTime(Value);
                }
                if (type.FullName == "System.Double")
                {
                    return Convert.ToDouble(Value);
                }
                if (type.FullName == "System.Boolean")
                {
                    return Convert.ToBoolean(Value);
                }
                if (type.IsEnum)
                {
                    return Enum.Parse(type, Convert.ToString(Value));
                }
                obj2 = Value;
            }
            catch (Exception exception)
            {
                throw new CommonException("类型转换失败", exception);
            }
            return obj2;
        }

        public FieldInfo GetFieldInfo(string FieldName)
        {
            FieldInfo field = this.m_type.GetField(FieldName, BindingFlags.Public | BindingFlags.Instance);
            if (field == null)
            {
                field = this.m_type.GetField(FieldName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (field == null)
                {
                    return null;
                }
            }
            return field;
        }

        public Type GetFieldType(string FieldName)
        {
            FieldInfo fieldInfo = this.GetFieldInfo(FieldName);
            if (fieldInfo == null)
            {
                return null;
            }
            return fieldInfo.FieldType;
        }

        private object GetFieldValue(string FieldName)
        {
            FieldInfo fieldInfo = this.GetFieldInfo(FieldName);
            if (fieldInfo == null)
            {
                return null;
            }
            return fieldInfo.GetValue(this.m_Instance);
        }

        public string GetOwnAssemblyName()
        {
            string fullName = null;
            try
            {
                fullName = Assembly.GetAssembly(this.m_type).FullName;
            }
            catch (Exception exception)
            {
                throw new CommonException("获得类的自己的程序集名异常", exception);
            }
            return fullName.Substring(0, fullName.IndexOf(","));
        }

        public PropertyInfo GetPropertyInfo(string PropertyName)
        {
            try
            {
                PropertyInfo property = this.m_type.GetProperty(PropertyName, BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                {
                    property = this.m_type.GetProperty(PropertyName, BindingFlags.NonPublic | BindingFlags.Instance);
                    if (property == null)
                    {
                        return null;
                    }
                }
                return property;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Type GetPropertyType(string PropertyName)
        {
            PropertyInfo propertyInfo = this.GetPropertyInfo(PropertyName);
            if (propertyInfo == null)
            {
                return null;
            }
            return propertyInfo.PropertyType;
        }

        private object GetPropertyValue(string PropertyName)
        {
            PropertyInfo propertyInfo = this.GetPropertyInfo(PropertyName);
            if (propertyInfo == null)
            {
                return null;
            }
            return propertyInfo.GetValue(this.m_Instance, null);
        }

        public Type GetType(string Name)
        {
            Type fieldType = this.GetFieldType(Name);
            if (fieldType == null)
            {
                fieldType = this.GetPropertyType(Name);
            }
            return fieldType;
        }

        public Type GetType()
        {
            return m_type;
        }

        public object GetValue(string Name)
        {
            object fieldValue = null;
            fieldValue = this.GetFieldValue(Name);
            if (fieldValue == null)
            {
                fieldValue = this.GetPropertyValue(Name);
            }
            return fieldValue;
        }

        public object Invoke(string MethodName)
        {
            return this.Invoke(MethodName, null);
        }

        public object Invoke(string MethodName, object[] Parameters)
        {
            object obj2;
            try
            {
                obj2 = this.m_Instance.GetType().GetMethod(MethodName).Invoke(this.m_Instance, Parameters);
            }
            catch (Exception exception)
            {
                throw new CommonException(MethodName + "方法调用失败", exception);
            }
            return obj2;
        }

        public object InvokeStatic(string MethodName)
        {
            return this.InvokeStatic(MethodName, null);
        }

        public object InvokeStatic(string MethodName, object[] Parameters)
        {
            object obj2;
            try
            {
                obj2 = this.m_type.GetMethod(MethodName).Invoke(null, Parameters);
            }
            catch (Exception exception)
            {
                throw new CommonException(MethodName + "方法调用失败", exception);
            }
            return obj2;
        }

        public bool IsExtendFrom(Type type)
        {
            Type baseType = this.m_type;
            while (true)
            {
                if (((baseType == null) || (baseType.FullName == type.FullName)) || (baseType.GetInterface(type.FullName) != null))
                {
                    break;
                }
                baseType = baseType.BaseType;
            }
            if (baseType == null)
            {
                return false;
            }
            return true;
        }

        public static Type LoadType(string className)
        {
            if (className != null)
            {
                className = className.Trim();
                Type type = null;
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                if (executingAssembly.GetType(className) != null)
                {
                    return executingAssembly.GetType(className);
                }
                type = Type.GetType(className);
                if (type != null)
                {
                    return type;
                }
                string[] strArray = className.Split(new char[] { '.' });
                string assemblyString = "";
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (assemblyString != "")
                    {
                        assemblyString = assemblyString + ".";
                    }
                    assemblyString = assemblyString + strArray[i];
                    executingAssembly = null;
                    try
                    {
                        type = Assembly.Load(assemblyString).GetType(className);
                        if (type != null)
                        {
                            return type;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return null;
        }

        private bool SetFieldValue(string FieldName, object Value)
        {
            FieldInfo fieldInfo = this.GetFieldInfo(FieldName);
            if (fieldInfo == null)
            {
                return false;
            }
            fieldInfo.SetValue(this.m_Instance, Value);
            return true;
        }

        private bool SetPropertyValue(string PropertyName, object Value)
        {
            PropertyInfo propertyInfo = this.GetPropertyInfo(PropertyName);
            if (propertyInfo == null)
            {
                return false;
            }
            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(this.m_Instance, Value, null);
            }
            return true;
        }

        public void SetValue(string Name, object Value)
        {
            Type type = this.GetType(Name);
            object obj2 = ConvertTo(Value, type);
            if (!this.SetFieldValue(Name, obj2))
            {
                this.SetPropertyValue(Name, obj2);
            }
        }

        // Properties
        public FieldInfo[] FieldInfos
        {
            get
            {
                return this.m_type.GetFields();
            }
        }

        public string GetTypeFullName
        {
            get
            {
                return this.m_type.FullName;
            }
        }

        public string GetTypeName
        {
            get
            {
                return this.GetTypeFullName.Substring(0, this.GetTypeFullName.LastIndexOf('.') + 1);
            }
        }

        public object Instance
        {
            get
            {
                return this.m_Instance;
            }
        }

        public MemberInfo[] Members
        {
            get
            {
                return this.m_type.GetMembers();
            }
        }

        public PropertyInfo[] PropertyInfos
        {
            get
            {
                return this.m_type.GetProperties();
            }
        }
    }






}