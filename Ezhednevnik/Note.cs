using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezhednevnik
{
    public class Note
    {
        public TimeSpan Time { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public enum NoteActionType
    {
        None,
        Enter,
        Esc,
        NextDate,
        LastDate,
    }
    public struct NoteAction
    {
        public NoteActionType ActionType { get; }
        public int Position { get; }

        public NoteAction(NoteActionType actionType, int position)
        {
            ActionType = actionType;
            Position = position;
        }
    }
    public enum State
    {
        MainMenu,
        DateMenu,
        NoteRead,
        NoteWrite,
        CloseCalendar
    }
    
}
