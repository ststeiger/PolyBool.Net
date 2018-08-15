
using Polybool.Net.Objects;

// https://github.com/voidqk/polybooljs
// https://github.com/idormenco/PolyBool.Net

namespace PolyBool.Net.Examples
{


    internal class Program
    {


        private static void Main(string[] args)
        {
            Polygon poly1 = new Polygon
            {
                Regions = new System.Collections.Generic.List<Region>
                { new Region{
                    Points = new System.Collections.Generic.List<Point>
                        {
                            new Point(200L,50L),
                            new Point(600L,50L),
                            new Point(600L,150L),
                            new Point(200L,150L)
                        }
                    }

                }
            };
            Polygon poly2 = new Polygon
            {
                Regions = new System.Collections.Generic.List<Region>
                { new Region{
                    Points = new System.Collections.Generic.List<Point>
                        {
                            new Point(300L,150L),
                            new Point(500L,90L),
                            new Point(500L,200L),
                            new Point(300L,200L)
                        }
                    }

                }
            };


            PolySegments seg1 = Polybool.Net.Logic.PolyBool.Segments(poly1);
            PolySegments seg2 = Polybool.Net.Logic.PolyBool.Segments(poly2);
            CombinedPolySegments comb = Polybool.Net.Logic.PolyBool.Combine(seg1, seg2);
            PolySegments seg3 = Polybool.Net.Logic.SegmentSelector.Difference(comb);
            PolySegments seg4 = Polybool.Net.Logic.SegmentSelector.Union(comb);

            Polygon polDiff = Polybool.Net.Logic.PolyBool.Polygon(seg3);
            Polygon polUn = Polybool.Net.Logic.PolyBool.Polygon(seg4);
            
            System.Console.WriteLine("Diff:{0} Union:{1}", polDiff, polUn);
        }
    }
}
