using System;

namespace dgmedia.Util.Attributes
{
    public class DescriptionAttribute : Attribute
    {
        private string _description;

        public string Description
        {
            get { return _description; }
        }

        public DescriptionAttribute(string description) : base()
        {
            _description = description;
        }
    }
}