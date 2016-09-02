using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross_Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Picross p = new Picross(5,5);

            //initialize rows with rules
            p.Rows = new List<Picross.Row>()
            {
                new Picross.Row(new int[]{ 1}),
                new Picross.Row(new int[] {1, 1}),
                new Picross.Row(new int[] {1, 1 }),
                new Picross.Row(new int[] {3, 1 }),
                new Picross.Row(new int[] {1, 1, 1})
            };

            //initialize columns with rules
            p.Columns = new List<Picross.Column>()
            {
                new Picross.Column(new int[] {4 }),
                new Picross.Column(new int[] {1}),
                new Picross.Column(new int[] {2}),
                new Picross.Column(new int[] {2}),
                new Picross.Column(new int[] {1,2})
            };

            p.initialize();

        }
    }
}
