using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SingleDispatchTest
{
    public class SingleDispatchTest
    {
        public class Pen
        {
            
        }


        public class Figure
        {
            private readonly StringBuilder _stringBuilder;

            public Figure(StringBuilder stringBuilder)
            {
                _stringBuilder = stringBuilder;
            }

            public void Draw(Pen pen)
            {
                _stringBuilder.AppendLine("Figure drawn in pen.");
            }

            public void Draw(object something)
            {
                _stringBuilder.AppendLine("Figure drawn with something.");
            }
        }

        [Fact]
        public void Test()
        {
            var sb = new StringBuilder();
            var figure = new Figure(sb);

            figure.Draw(new Pen());
            object rallyAPen = new Pen();
            figure.Draw(rallyAPen);

            var result = sb.ToString();

            Assert.Equal(@"Figure drawn in pen." + Environment.NewLine +
                         "Figure drawn with something.", result);
        }
    }
}
