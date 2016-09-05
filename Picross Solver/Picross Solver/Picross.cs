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
            int width = Board.GetLength(0);
            int height = Board.GetLength(1);

            foreach (Row row in Rows)
            {
                row.Known = new bool?[width];

                List<bool[]> rowPossibilities = row.Possibilities;
                generatePossibilities(row.Rules, width, ref rowPossibilities);
            }

            foreach (Column col in Columns)
            {
                col.Known = new bool?[height];

                List<bool[]> possibilities = col.Possibilities;
                generatePossibilities(col.Rules, height, ref possibilities);
            }

            int counter = 0;
            while (true)
            {
                printBoard(counter++);
                for (int row = 0; row < height; row++)
                {
                    // update row's know state from board's know state
                    for (int i = 0; i < width; i++)
                        if (Board[i, row] != null)
                            Rows[row].Known[i] = Board[i, row];

                    // if row is solved
                    if (Rows[row].Possibilities.Count == 1)
                    {
                        for (int i = 0; i < width; i++)
                            Rows[row].Known[i] = Rows[row].Possibilities[0][i] == true;

                        Rows[row].Possibilities.Clear();
                    }

                    // find definate positives
                    bool[] positives = Enumerable.Repeat(true, width).ToArray();
                    foreach (bool[] possibility in Rows[row].Possibilities)
                        for (int i = 0; i < width; i++)
                            positives[i] = positives[i] && possibility[i];

                    // set definate positives on board
                    for (int i = 0; i < width; i++)
                        if (positives[i])
                            Board[i, row] = true;

                    // find definate negatives
                    bool[] negatives = Enumerable.Repeat(false, width).ToArray();
                    foreach (bool[] possibility in Rows[row].Possibilities)
                        for (int i = 0; i < width; i++)
                            negatives[i] = negatives[i] || possibility[i];

                    // set definate negatives on board
                    for (int i = 0; i < width; i++)
                        if (!negatives[i])
                            Board[i, row] = false;


                    List<bool[]> possibilities = new List<bool[]>();
                    possibilities = Rows[row].Possibilities.GetRange(0, Rows[row].Possibilities.Count);

                    int colIndex = 0;
                    foreach (bool[] possibility in possibilities)
                    {
                        bool remove = false;
                        for (int j = 0; j < height; j++)
                        {
                            if (Board[j, row] != null && Board[j, row] != possibility[j])
                                remove = true;
                        }
                        if (remove)
                            Rows[row].Possibilities.RemoveAt(colIndex);
                        else
                            colIndex++;

                    }
                    //// remove possibilities that conflicts with board
                    //for (int i = 0; i < Rows[row].Possibilities.Count; i++)
                    //    for (int j = 0; j < width; j++)
                    //        if (Board[j, row] != null && Board[j, row] != Rows[row].Possibilities[i][j])
                    //            Rows[row].Possibilities.RemoveAt(i);
                }


                for (int col = 0; col < height; col++)
                {
                    // update column's know state from board's know state
                    for (int i = 0; i < height; i++)
                        if (Board[i, col] != null)
                            Columns[i].Known[col] = Board[i, col];

                    bool?[] columnKnown = Enumerable.Repeat((bool?)null, height).ToArray();
                    for (int i = 0; i < height; i++)
                        columnKnown[i] = Board[col, i];

                    // if column is solved
                    if (Columns[col].Possibilities.Count == 1)
                    {
                        for (int i = 0; i < height; i++)
                            Columns[col].Known[i] = Columns[col].Possibilities[0][i] == true;

                        Columns[col].Possibilities.Clear();
                    }

                    // find definate positives
                    bool[] positives = Enumerable.Repeat(true, height).ToArray();
                    foreach (bool[] possibility in Columns[col].Possibilities)
                        for (int i = 0; i < height; i++)
                            positives[i] = positives[i] && possibility[i];

                    // set definate positives on board
                    for (int i = 0; i < height; i++)
                        if (positives[i])
                            Board[col, i] = true;

                    // find definate negatives
                    bool[] negatives = Enumerable.Repeat(false, height).ToArray();
                    foreach (bool[] possibility in Columns[col].Possibilities)
                        for (int i = 0; i < height; i++)
                            negatives[i] = negatives[i] || possibility[i];

                    // set definate negatives on board
                    for (int i = 0; i < height; i++)
                        if (!negatives[i])
                            Board[col, i] = false;

                    // remove possibilities that conflicts with board
                    List<bool[]> possibilities = new List<bool[]>();
                    possibilities = Columns[col].Possibilities.GetRange(0, Columns[col].Possibilities.Count);

                    int colIndex = 0;
                    foreach (bool[] possibility in possibilities)
                    {
                        bool remove = false;
                        for (int j = 0; j < height; j++)
                        {
                            if (Board[col, j] != null && Board[col, j] != possibility[j])
                                remove = true;
                        }
                        if (remove)
                            Columns[col].Possibilities.RemoveAt(colIndex);
                        else
                            colIndex++;

                    }

                }

            }

        }

        private void printBoard(int counter)
        {
            Console.Clear();
            Console.WriteLine(counter);
            for (int y = 0; y < Board.GetLength(1); y++)
            {
                for (int x = 0; x < Board.GetLength(0); x++)
                {
                    Console.Write(Board[x, y] == null ? "." : Board[x, y] == true ? "X" : "Y");
                }
                Console.WriteLine();
            }
        }

        private List<bool[]> generatePossibilities(int[] rule, int width, ref List<bool[]> possibilities, int[] gaps = null, int gapIndex = 0)
        {
            #region init
            if (gaps == null)
            {
                gaps = Enumerable.Repeat(1, rule.Length).ToArray();
                gaps[0] = 0;
            }

            #endregion



            bool[] state = Enumerable.Repeat(false, width).ToArray();

            int pos = 0;
            for (int i = 0; i < rule.Length; i++)
            {
                for (int j = 0; j < gaps[i]; j++)
                    state[pos++] = false;

                for (int j = 0; j < rule[i]; j++)
                    state[pos++] = true;

            }
            bool contains = false;
            foreach (bool[] item in possibilities)
            {
                contains = contains || item.SequenceEqual(state);
            }

            if (!contains)
                possibilities.Add(state);

            if (rule.Sum() + gaps.Sum() < width)
            {
                int[] gapsToProcess = new int[gaps.Length];
                Array.Copy(gaps, gapsToProcess, gaps.Length);
                gapsToProcess[gapIndex]++;
                generatePossibilities(rule, width, ref possibilities, gapsToProcess, gapIndex);
            }

            if (gapIndex < gaps.Length - 1)
            {
                gapIndex++;
                int[] gapsToProcess = new int[gaps.Length];
                Array.Copy(gaps, gapsToProcess, gaps.Length);
                gapsToProcess[gapIndex]++;
                generatePossibilities(rule, width, ref possibilities, gaps, gapIndex);
            }
            return possibilities;
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
                        ret.Append("{" + string.Join(",", item) + "}\r\n");
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
