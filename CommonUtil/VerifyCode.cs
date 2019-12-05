using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace CommonUtil
{

    public class VerifyCode
    {

        #region 变量

        /// <summary>
        /// 验证代码的位数
        /// </summary>
        private int m_CodeLength;

        /// <summary>
        /// 输出图片的宽度
        /// </summary>
        private int m_ImageWidth;

        /// <summary>
        /// 输出图片的高度
        /// </summary>
        private int m_ImageHeight;

        /// <summary>
        /// 字符字典
        /// </summary>
        private VerifyCodeCharDictionary m_CharDictionary;

        /// <summary>
        /// 字符扭曲度
        /// </summary>
        private int m_WarpFactor;

        /// <summary>
        /// 噪音线
        /// </summary>
        private int m_NoiseLine;

        /// <summary>
        /// 噪音点
        /// </summary>
        private int m_NoisePoint;

        /// <summary>
        /// 是否绘制边框
        /// </summary>
        private bool m_DrawBorder;

        private string m_CodeValue;

        #endregion


        #region 构造函数

        public VerifyCode()
        {
            m_CodeLength = 4;
            m_ImageWidth = 50;
            m_ImageHeight = 20;
            m_CharDictionary = VerifyCodeCharDictionary.SafeLetters;
            m_WarpFactor = 5;
            m_NoiseLine = 20;
            m_NoisePoint = 100;
            m_DrawBorder = true;
        }

        #endregion


        #region 属性

        /// <summary>
        /// 输出随机字符串的位数，默认4
        /// </summary>
        public int CodeLength
        {
            set
            {
                m_CodeLength = value;
            }
            get
            {
                return m_CodeLength;
            }
        }

        /// <summary>
        /// 输出图片的宽度，默认50，自动宽度
        /// </summary>
        public int ImageWidth
        {
            set
            {
                m_ImageWidth = value;
            }
            get
            {
                return m_ImageWidth;
            }
        }

        /// <summary>
        /// 输出图片的高度，默认20，自动高度
        /// </summary>
        public int ImageHeight
        {
            set
            {
                m_ImageHeight = value;
            }
            get
            {
                return m_ImageHeight;
            }
        }

        /// <summary>
        /// 字符字典，默认为SafeLetters，全数字和大写字母
        /// </summary>
        public VerifyCodeCharDictionary CharDictionary
        {
            set
            {
                m_CharDictionary = value;
            }
            get
            {
                return m_CharDictionary;
            }
        }

        /// <summary>
        /// 字符扭曲度，默认为5，数字越大，扭曲越厉害
        /// </summary>
        public int WarpFactor
        {
            set
            {
                m_WarpFactor = value;
            }
            get
            {
                return m_WarpFactor;
            }
        }

        /// <summary>
        /// 噪音线，默认为20，数字越大，噪音线越多
        /// </summary>
        public int NoiseLine
        {
            set
            {
                m_NoiseLine = value;
            }
            get
            {
                return m_NoiseLine;
            }
        }

        /// <summary>
        /// 噪音点，默认为100，数字越大，噪音点越多
        /// </summary>
        public int NoisePoint
        {
            set
            {
                m_NoisePoint = value;
            }
            get
            {
                return m_NoisePoint;
            }
        }

        /// <summary>
        /// 是否绘制边框，默认为true，绘制
        /// </summary>
        public bool DrawBorder
        {
            set
            {
                m_DrawBorder = value;
            }
            get
            {
                return m_DrawBorder;
            }
        }

        #endregion


        #region 方法

        /// <summary>
        /// 生成随机数
        /// </summary>
        public void Generator()
        {
            m_CodeValue = GeneratorRandomCode();
        }

        /// <summary>
        /// 获取Code值
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            return m_CodeValue;
        }

        /// <summary>
        /// 获取Code图片的Base64Src
        /// </summary>
        /// <returns></returns>
        public string GetImageBase64Src()
        {
            return "data:image/bmp;base64," + CommonUtil.ConvertBytes.ToBase64(GetImageBuffer());
        }

        #endregion


        #region 私有方法

        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <returns></returns>
        private string GeneratorRandomCode()
        {
            try
            {
                string[] charNumbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                string[] charLowerLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                string[] charUpperLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                string[] charSafeLetters = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "C", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

                //验证码中所有的字符
                string[] charArray;
                switch (m_CharDictionary)
                {
                    case VerifyCodeCharDictionary.Numbers:
                        charArray = charNumbers;
                        break;
                    case VerifyCodeCharDictionary.Letters:
                        charArray = ConnectArrray.ByString(charLowerLetters, charUpperLetters);
                        break;
                    case VerifyCodeCharDictionary.LowerLetters:
                        charArray = charLowerLetters;
                        break;
                    case VerifyCodeCharDictionary.UpperLetters:
                        charArray = charUpperLetters;
                        break;
                    case VerifyCodeCharDictionary.NumbersAndLetters:
                        charArray = ConnectArrray.ByString(charNumbers, charLowerLetters, charUpperLetters);
                        break;
                    case VerifyCodeCharDictionary.NumbersAndLowerLetters:
                        charArray = ConnectArrray.ByString(charNumbers, charLowerLetters);
                        break;
                    case VerifyCodeCharDictionary.NumbersAndUpperLetters:
                        charArray = ConnectArrray.ByString(charNumbers, charUpperLetters);
                        break;
                    default:
                        charArray = charSafeLetters;
                        break;
                }

                string randomcode = "";
                //生成一个随机对象
                Random rand = new Random();
                //根据验证码的位数循环
                for (int i = 0; i < m_CodeLength; i++)
                {
                    int t = rand.Next(charArray.Length);
                    randomcode += charArray[t];
                }
                //返回生成的随机字符
                return randomcode;
            }
            catch
            {
            }
            return "";
        }

        /// <summary>
        /// 获取Code图片二进制
        /// </summary>
        /// <returns></returns>
        private byte[] GetImageBuffer()
        {
            string FontFamily = "Arial";
            Brush Foreground = Brushes.Navy;
            Color Background = Color.Silver;

            if (m_CodeValue != "")
            {
                using (Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight))
                using (Graphics g = Graphics.FromImage(bmp))
                using (Font font = new Font(FontFamily, 1f))
                {
                    g.Clear(Background);
                    // Perform trial rendering to determine best font size
                    SizeF finalSize;
                    SizeF testSize = g.MeasureString(m_CodeValue, font);
                    float bestFontSize = Math.Min(m_ImageWidth / testSize.Width,
                    m_ImageHeight / testSize.Height);// *0.95f;
                    using (Font finalFont = new Font(FontFamily, bestFontSize))
                    {
                        finalSize = g.MeasureString(m_CodeValue, finalFont);
                    }
                    // Get a path representing the text centered on the canvas
                    g.PageUnit = GraphicsUnit.Point;
                    PointF textTopLeft = new PointF((m_ImageWidth - finalSize.Width) / 2,
                    (m_ImageHeight - finalSize.Height) / 2);
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddString(m_CodeValue, new FontFamily(FontFamily), 0,
                        bestFontSize, textTopLeft, StringFormat.GenericDefault);

                        //生成随机生成器 
                        Random random = new Random();

                        //清空图片背景色 
                        g.Clear(Color.White);

                        //画图片的背景噪音线 
                        for (int i = 0; i < m_NoiseLine; i++)
                        {
                            int x1 = random.Next(bmp.Width);
                            int x2 = random.Next(bmp.Width);
                            int y1 = random.Next(bmp.Height);
                            int y2 = random.Next(bmp.Height);
                            g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                        }


                        // Render the path to the bitmap
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        //未处理正常字体 g.FillPath(Foreground, path);
                        g.FillPath(Foreground, DeformPath(path));

                        //画图片的前景噪音点 
                        for (int i = 0; i < m_NoisePoint; i++)
                        {
                            int x = random.Next(bmp.Width);
                            int y = random.Next(bmp.Height);

                            bmp.SetPixel(x, y, Color.FromArgb(random.Next()));
                        }

                        //画图片的边框线 
                        if (m_DrawBorder)
                        {
                            g.DrawRectangle(new Pen(Color.Silver), 0, 0, bmp.Width - 1, bmp.Height - 1);
                        }

                        g.Flush();


                        //将图像保存到指定的流
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                        return ms.GetBuffer();
                    }
                }
            }

            return new byte[0];
        }

        /// <summary>
        /// 扭曲文字
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private GraphicsPath DeformPath(GraphicsPath path)
        {
            Double xAmp = m_WarpFactor * m_ImageWidth / 100;
            Double yAmp = m_WarpFactor * m_ImageHeight / 85;
            Double xFreq = 2 * Math.PI / m_ImageWidth;
            Double yFreq = 2 * Math.PI / m_ImageHeight;

            PointF[] deformed = new PointF[path.PathPoints.Length];
            Random rng = new Random();
            Double xSeed = rng.NextDouble() * 2 * Math.PI;
            Double ySeed = rng.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < path.PathPoints.Length; i++)
            {
                PointF original = path.PathPoints[i];
                Double val = xFreq * original.X + yFreq * original.Y;
                int xOffset = (int)(xAmp * Math.Sin(val + xSeed));
                int yOffset = (int)(yAmp * Math.Sin(val + ySeed));
                deformed[i] = new PointF(original.X + xOffset, original.Y + yOffset);
            }
            return new GraphicsPath(deformed, path.PathTypes);
        }

        #endregion

    }


    /// <summary>
    /// 字符字典
    /// </summary>
    public enum VerifyCodeCharDictionary
    {
        Numbers,
        Letters,
        LowerLetters,
        UpperLetters,
        NumbersAndLetters,
        NumbersAndLowerLetters,
        NumbersAndUpperLetters,
        SafeLetters
    }
}
