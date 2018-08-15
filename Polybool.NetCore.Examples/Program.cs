
using Polybool.Net.Objects;

// https://github.com/voidqk/polybooljs
// https://github.com/idormenco/PolyBool.Net

namespace Polybool.NetCore.Examples
{


    internal class Program
    {


        private static void Main(string[] args)
        {
            System.Collections.Generic.List<Point> points1 = 
                new System.Collections.Generic.List<Point>
            {
                 new Point(9.3763619m,47.4330074m)
                ,new Point(9.3764389m,47.4329684m)
                ,new Point(9.3764072m,47.4329405m)
                ,new Point(9.3763969m,47.4329322m)
                ,new Point(9.3759864m,47.4326004m)
                ,new Point(9.376056m,47.4325644m)
                ,new Point(9.3761349m,47.4325167m)
                ,new Point(9.37619m,47.4325653m)
                ,new Point(9.376312m,47.4326732m)
                ,new Point(9.3765907m,47.4328683m)
                ,new Point(9.3766389m,47.4328521m)
                ,new Point(9.3767794m,47.4329452m)
                ,new Point(9.3764748m,47.4331106m)
                ,new Point(9.3763619m,47.4330074m)
            };


            System.Collections.Generic.List<Point> points2 =
                new System.Collections.Generic.List<Point>
            {
                 new Point(9.3766833m,47.4319973m)
                ,new Point(9.3772045m,47.4324131m)
                ,new Point(9.3771257m,47.432459m)
                ,new Point(9.3769959m,47.4323535m)
                ,new Point(9.3767225m,47.4325076m)
                ,new Point(9.3768938m,47.432637m)
                ,new Point(9.3769843m,47.4325975m)
                ,new Point(9.3772713m,47.432826m)
                ,new Point(9.3771862m,47.4328789m)
                ,new Point(9.376941m,47.4326789m)
                ,new Point(9.3767283m,47.4327757m)
                ,new Point(9.3765053m,47.4325749m)
                ,new Point(9.376312m,47.4326732m)
                ,new Point(9.37619m,47.4325653m)
                ,new Point(9.3761349m,47.4325167m)
                ,new Point(9.376056m,47.4325644m)
                ,new Point(9.3757946m,47.43237m)
                ,new Point(9.3760399m,47.4322419m)
                ,new Point(9.376144m,47.4323272m)
                ,new Point(9.3761809m,47.4323125m)
                ,new Point(9.3762975m,47.432428m)
                ,new Point(9.3762371m,47.4324602m)
                ,new Point(9.3763095m,47.4325246m)
                ,new Point(9.3764699m,47.4324328m)
                ,new Point(9.3763633m,47.4323437m)
                ,new Point(9.3763976m,47.4323193m)
                ,new Point(9.3763247m,47.4322628m)
                ,new Point(9.3763972m,47.4322251m)
                ,new Point(9.3764363m,47.4322061m)
                ,new Point(9.3766528m,47.4323718m)
                ,new Point(9.3768611m,47.4322514m)
                ,new Point(9.3765976m,47.4320409m)
                ,new Point(9.3766833m,47.4319973m)
            };

            //points1.Reverse();
            //points2.Reverse();

            Polygon poly1 = new Polygon
            {
                Regions = new System.Collections.Generic.List<Region>
                {
                    new Region { Points = points1 }
                }
            };

            Polygon poly2 = new Polygon
            {
                Regions = new System.Collections.Generic.List<Region>
                {
                    new Region{ Points = points2 }
                }
            };


            PolySegments seg1 = Polybool.Net.Logic.PolyBool.Segments(poly1);
            PolySegments seg2 = Polybool.Net.Logic.PolyBool.Segments(poly2);
            CombinedPolySegments comb = Polybool.Net.Logic.PolyBool.Combine(seg1, seg2);
            PolySegments seg3 = Polybool.Net.Logic.SegmentSelector.Difference(comb);
            PolySegments seg4 = Polybool.Net.Logic.SegmentSelector.Union(comb);

            Polygon polDiff = Polybool.Net.Logic.PolyBool.Polygon(seg3);
            Polygon polUn = Polybool.Net.Logic.PolyBool.Polygon(seg4);

            System.Collections.Generic.List<Segment> polUn2 = 
                Polybool.Net.Logic.SegmentSelector.Union(comb.Combined);

            int c = points1.Count + points2.Count;
            System.Console.WriteLine("The combination would have {0} points. Actual: {1}", c, polUn2.Count * 2);


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("POLYGON((");

            bool isNotFirst = false;
            foreach (Segment s in polUn2)
            {
                System.Console.WriteLine("{0} {1}", s.Start.X, s.Start.Y);
                System.Console.WriteLine("{0} {1}", s.End.X, s.End.Y);

                if (isNotFirst)
                    sb.Append(",");
                else
                    isNotFirst = true;

                sb.Append(s.Start.X.ToString(System.Globalization.CultureInfo.InvariantCulture));
                sb.Append(" ");
                sb.Append(s.Start.Y.ToString(System.Globalization.CultureInfo.InvariantCulture));

                sb.Append(",");

                sb.Append(s.Start.X.ToString(System.Globalization.CultureInfo.InvariantCulture));
                sb.Append(" ");
                sb.Append(s.Start.Y.ToString(System.Globalization.CultureInfo.InvariantCulture));
            } // Next s 

            sb.Append("))");
            string str = sb.ToString();
            sb.Clear();
            sb = null;
            System.Console.WriteLine(str);


            foreach (Region thisRegion in polUn.Regions)
            {
                foreach (Point p in thisRegion.Points)
                {
                    System.Console.WriteLine("{0} {1}", p.X, p.Y);
                }
            }

            System.Console.WriteLine("Diff:{0} Union:{1}", polDiff, polUn);
        }
    }
}
