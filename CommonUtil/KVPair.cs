using System;

namespace CommonUtil
{
    public class KVPair
    {
        public const string F_KEY = "Key";
        public const string F_VALUE = "Value";

        private System.String m_Key;
        private System.Object m_Value;

        public KVPair()
            : this("", new Object())
        {
        }

        public KVPair(String key, Object val)
        {
            m_Key = key;
            m_Value = val;
        }

        #region Attributes

        public System.String Key
        {
            get
            {
                return m_Key;
            }
            set
            {
                m_Key = value;
            }
        }
        public System.Object Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }

        #endregion

    }
}
