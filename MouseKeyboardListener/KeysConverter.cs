using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

using static System.ComponentModel.TypeConverter;

namespace MouseKeyboardListener
{
    public class KeysConverter : TypeConverter, IComparer
    {
        private const Keys FirstDigit = Keys.D0;

        private const Keys LastDigit = Keys.D9;

        private const Keys FirstAscii = Keys.A;

        private const Keys LastAscii = Keys.Z;

        private const Keys FirstNumpadDigit = Keys.NumPad0;

        private const Keys LastNumpadDigit = Keys.NumPad9;

        private IDictionary keyNames;

        private List<string> displayOrder;

        private StandardValuesCollection values;

        private IDictionary KeyNames
        {
            get
            {
                if (keyNames == null)
                {
                    Initialize();
                }

                return keyNames;
            }
        }

        private List<string> DisplayOrder
        {
            get
            {
                if (displayOrder == null)
                {
                    Initialize();
                }

                return displayOrder;
            }
        }

        private void AddKey(string key, Keys value)
        {
            keyNames[key] = value;
            displayOrder.Add(key);
        }

        private void Initialize()
        {
            keyNames = new Hashtable(34);
            displayOrder = new List<string>(34);
            AddKey("Enter", Keys.Return);
            AddKey("F12", Keys.F12);
            AddKey("F11", Keys.F11);
            AddKey("F10", Keys.F10);
            AddKey("End", Keys.End);
            AddKey("Control", Keys.Control);
            AddKey("F8", Keys.F8);
            AddKey("F9", Keys.F9);
            AddKey("Alt", Keys.Alt);
            AddKey("F4", Keys.F4);
            AddKey("F5", Keys.F5);
            AddKey("F6", Keys.F6);
            AddKey("F7", Keys.F7);
            AddKey("F1", Keys.F1);
            AddKey("F2", Keys.F2);
            AddKey("F3", Keys.F3);
            AddKey("PageDown", Keys.Next);
            AddKey("Insert", Keys.Insert);
            AddKey("Home", Keys.Home);
            AddKey("Delete", Keys.Delete);
            AddKey("Shift", Keys.Shift);
            AddKey("PageUp", Keys.Prior);
            AddKey("Back", Keys.Back);
            AddKey("0", Keys.D0);
            AddKey("1", Keys.D1);
            AddKey("2", Keys.D2);
            AddKey("3", Keys.D3);
            AddKey("4", Keys.D4);
            AddKey("5", Keys.D5);
            AddKey("6", Keys.D6);
            AddKey("7", Keys.D7);
            AddKey("8", Keys.D8);
            AddKey("9", Keys.D9);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(Enum[]))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Enum[]))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public int Compare(object a, object b)
        {
            return string.Compare(ConvertToString(a), ConvertToString(b), ignoreCase: false, CultureInfo.InvariantCulture);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string text = ((string)value).Trim();
                if (text.Length == 0)
                {
                    return null;
                }

                string[] array = text.Split('+');
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array[i].Trim();
                }

                Keys keys = Keys.None;
                bool flag = false;
                for (int j = 0; j < array.Length; j++)
                {
                    object obj = KeyNames[array[j]];
                    if (obj == null)
                    {
                        obj = Enum.Parse(typeof(Keys), array[j]);
                    }

                    if (obj != null)
                    {
                        Keys keys2 = (Keys)obj;
                        if ((keys2 & Keys.KeyCode) != 0)
                        {
                            if (flag)
                            {
                                throw new FormatException("Invalid Key Combination");
                            }

                            flag = true;
                        }

                        keys |= keys2;
                        continue;
                    }

                    throw new FormatException("Invalid Key Name");
                }

                return keys;
            }

            if (value is Enum[])
            {
                long num = 0L;
                Enum[] array2 = (Enum[])value;
                foreach (Enum value2 in array2)
                {
                    num |= Convert.ToInt64(value2, CultureInfo.InvariantCulture);
                }

                return Enum.ToObject(typeof(Keys), num);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            if (value is Keys || value is int)
            {
                bool flag = destinationType == typeof(string);
                bool flag2 = false;
                if (!flag)
                {
                    flag2 = destinationType == typeof(Enum[]);
                }

                if (flag || flag2)
                {
                    Keys keys = (Keys)value;
                    bool flag3 = false;
                    ArrayList arrayList = new ArrayList();
                    Keys keys2 = keys & Keys.Modifiers;
                    for (int i = 0; i < DisplayOrder.Count; i++)
                    {
                        string text = DisplayOrder[i];
                        Keys keys3 = (Keys)keyNames[text];
                        if ((keys3 & keys2) == 0)
                        {
                            continue;
                        }

                        if (flag)
                        {
                            if (flag3)
                            {
                                arrayList.Add("+");
                            }

                            arrayList.Add(text);
                        }
                        else
                        {
                            arrayList.Add(keys3);
                        }

                        flag3 = true;
                    }

                    Keys keys4 = keys & Keys.KeyCode;
                    bool flag4 = false;
                    if (flag3 && flag)
                    {
                        arrayList.Add("+");
                    }

                    for (int j = 0; j < DisplayOrder.Count; j++)
                    {
                        string text2 = DisplayOrder[j];
                        Keys keys5 = (Keys)keyNames[text2];
                        if (keys5.Equals(keys4))
                        {
                            if (flag)
                            {
                                arrayList.Add(text2);
                            }
                            else
                            {
                                arrayList.Add(keys5);
                            }

                            flag3 = true;
                            flag4 = true;
                            break;
                        }
                    }

                    if (!flag4 && Enum.IsDefined(typeof(Keys), (int)keys4))
                    {
                        if (flag)
                        {
                            arrayList.Add(keys4.ToString());
                        }
                        else
                        {
                            arrayList.Add(keys4);
                        }
                    }

                    if (flag)
                    {
                        StringBuilder stringBuilder = new StringBuilder(32);
                        foreach (string item in arrayList)
                        {
                            stringBuilder.Append(item);
                        }

                        return stringBuilder.ToString();
                    }

                    return (Enum[])arrayList.ToArray(typeof(Enum));
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (values == null)
            {
                ArrayList arrayList = new ArrayList();
                ICollection collection = KeyNames.Values;
                foreach (object item in collection)
                {
                    arrayList.Add(item);
                }

                arrayList.Sort(this);
                values = new StandardValuesCollection(arrayList.ToArray());
            }

            return values;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
