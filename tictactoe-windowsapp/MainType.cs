using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe_windowsapp
{
    public enum MarkType
    {
        /// <summary>
        /// The cell hasn't been selected
        /// </summary>
        Free,
        /// <summary>
        /// The cell  has been selected by 'O' player
        /// </summary>
        Nought,
        /// <summary>
        /// The cell has ben selected by 'X' player
        /// </summary>
        Cross
    }
}
