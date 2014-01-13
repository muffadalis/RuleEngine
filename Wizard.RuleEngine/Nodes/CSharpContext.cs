using System;
using System.Collections.Generic;

namespace Wizard.RuleEngine.Nodes
{
    public class CSharpContext
    {
        public Dictionary<string, object> Tags { get; set; }

        public CSharpContext()
        {
            Tags = new Dictionary<string, object>();
        }

        int _indentation;
        public int Indentation
        {
            get { return _indentation; }
            set { _indentation = (value < 0) ? 0 : _indentation = value; }
        }

        public string IndentationText
        {
            get { return new String('\t', Indentation); }
        }
    }
}