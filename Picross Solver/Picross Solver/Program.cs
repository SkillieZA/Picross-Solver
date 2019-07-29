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
            Picross p = new Picross(30,20);


            //initialize rows with rules
            p.Rows = new List<Picross.Row>()
            {
                new Picross.Row(new int[] { 1}),
                new Picross.Row(new int[] {2,3}),
                new Picross.Row(new int[] {4,2,1 }),
                new Picross.Row(new int[] {1,2,2,1 }),
                new Picross.Row(new int[] {2,2,2,2}),
                new Picross.Row(new int[] {2,2,7}),
                new Picross.Row(new int[] {4,9}),
                new Picross.Row(new int[] {2,8,9}),
                new Picross.Row(new int[] {3,16,3,1}),
                new Picross.Row(new int[] {26}),
                new Picross.Row(new int[] {26}),
                new Picross.Row(new int[] {26}),
                new Picross.Row(new int[] {23,1,1}),
                new Picross.Row(new int[] {27}),
                new Picross.Row(new int[] {21,3}),
                new Picross.Row(new int[] {7,3,6}),
                new Picross.Row(new int[] {5,5}),
                new Picross.Row(new int[] {4,5}),
                new Picross.Row(new int[] {4,4}),
                new Picross.Row(new int[] {3,3})
            };

            //initialize columns with rules
            p.Columns = new List<Picross.Column>()
            {
                new Picross.Column(new int[] {3 }),
                new Picross.Column(new int[] {5 }),
                new Picross.Column(new int[] {2,2 }),
                new Picross.Column(new int[] {1,2,4 }),
                new Picross.Column(new int[] {11 }),
                new Picross.Column(new int[] {12 }),
                new Picross.Column(new int[] {12 }),
                new Picross.Column(new int[] {13 }),
                new Picross.Column(new int[] {10 }),
                new Picross.Column(new int[] {9 }),
                new Picross.Column(new int[] {8 }),
                new Picross.Column(new int[] {9 }),
                new Picross.Column(new int[] {9 }),
                new Picross.Column(new int[] {9 }),
                new Picross.Column(new int[] {4,8 }),
                new Picross.Column(new int[] {2,2,7 }),
                new Picross.Column(new int[] {1,2,11 }),
                new Picross.Column(new int[] {2,14 }),
                new Picross.Column(new int[] {2,14 }),
                new Picross.Column(new int[] {16 }),
                new Picross.Column(new int[] {3,9 }),
                new Picross.Column(new int[] {11 }),
                new Picross.Column(new int[] {12 }),
                new Picross.Column(new int[] {13 }),
                new Picross.Column(new int[] {2,3,5 }),
                new Picross.Column(new int[] {2,2,7 }),
                new Picross.Column(new int[] {4,3,2 }),
                new Picross.Column(new int[] {5 }),
                new Picross.Column(new int[] {2,2 }),
                new Picross.Column(new int[] {3 })
            };
            
            /*//initialize rows with rules
            p.Rows = new List<Picross.Row>()
            {
                new Picross.Row(new int[] { 1}),
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
            };*/

            p.initialize();

        }
    }
}
