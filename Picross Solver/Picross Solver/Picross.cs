using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross_Solver
{
    class Picross
    {
        private bool?[,] _board;
        private List<Row> _rows;
        private List<Column> _columns;

        public bool?[,] Board
        {
            get { return _board; }
            set { _board = value; }
        }

        public List<Row> Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }


        public List<Column> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public Picross(int width, int height)
        {
            _board = new bool?[width, height];
        }

        public void initialize()
        {
            foreach (Row row in Rows)
            {
                int width = Board.GetLength(0);
                row.Known = new bool?[width];

                int possibilities = width - row.Rules.Sum() + row.Rules.Length - 1;
                int shortestLength = row.Rules.Sum() + row.Rules.Length - 1;

                if (width == shortestLength)
                {
                    List<bool?> temp = new List<bool?>();
                    foreach (int rule in row.Rules)
                    {
                        for (int i = 0; i < rule; i++)
                        {
                            temp.Add(true);
                        }
                        temp.Add(false);
                    }
                    temp.RemoveAt(temp.Count - 1);
                    row.Known = temp.ToArray<bool?>();
                }
                else
                {

                    // rather factor this out to a recursive method to add False values at a certain
                    // gap between True values
                    // generatePossibilities(int[] rules, int[] gaps, int gapIndex) { }


                    // This will be the amount of spaces before
                    for (int spaceBefore = 0; spaceBefore < width - shortestLength + 1; spaceBefore++)
                    {
                        List<bool> temp = new List<bool>();
                        for (int j = 0; j < spaceBefore; j++)
                        {
                            temp.Add(false);
                        }
                        foreach (int rule in row.Rules)
                        {
                            for (int i = 0; i < rule; i++)
                            {
                                temp.Add(true);
                            }
                            temp.Add(false);
                        }
                        temp.RemoveAt(temp.Count - 1);
                        while (temp.Count < width)
                        {
                            temp.Add(false);
                        }
                        if (temp.Count == width)
                            row.Possibilities.Add(temp.ToArray<bool>());
                        else
                        {
                            string s = "";
                        }
                    }
                }
            }
        }



        public class Row
        {
            private int[] _rules;
            private List<bool[]> _possibilities;
            public bool?[] Known { get; set; }
            public List<bool[]> Possibilities
            {
                get { return _possibilities; }
                set { _possibilities = value; }
            }

            public string PossibilitiesString
            {
                get
                {
                    StringBuilder ret = new StringBuilder();
                    foreach (bool[] item in _possibilities)
                    {
                        ret.Append("{" + string.Join(",", item) + "},");
                    }
                    return ret.ToString().Substring(0, ret.ToString().Length - 1);
                }
            }


            public int[] Rules
            {
                get { return _rules; }
                set { _rules = value; }
            }

            public Row(int[] rules)
            {
                _rules = rules;
                Possibilities = new List<bool[]>();
            }
        }
        public class Column
        {
            private int[] _rules;
            private bool?[] _known;
            public bool?[] Known
            {
                get { return _known; }
                set { _known = value; }
            }
            public List<bool[]> Possibilities { get; set; }

            public int[] Rules
            {
                get { return _rules; }
                set { _rules = value; }
            }

            public Column(int[] rules)
            {
                _rules = rules;
                Possibilities = new List<bool[]>();

            }

        }
    }
}
