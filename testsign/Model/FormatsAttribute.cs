using System;

namespace testsign.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatsAttribute : Attribute
    {
        public FormatsAttribute(string format)
        {
            Format = format;
        }

        public string Format { get; }
    }
}