using Ezhednevnik;
internal class Program
{
    public static List<Note> Notes = new List<Note>()
    {
        new Note()
        {
            Name = "Проснуться",
            Time = TimeSpan.FromHours(6),
            Description = "Прочнуться в 6 утра"
        },
        new Note()
        {
            Name = "Зарядка",
            Time = TimeSpan.FromHours(7),
            Description = "Сделать зарядку на все группы мыщц. Добавить пробежку"
        },
        new Note()
        {
            Name = "Учёба",
            Time = TimeSpan.FromHours(10),
            Description = "Собрать рюкзак и пойти на учёбу"
        },
        new Note()
        {
            Name = "Уроки",
            Time = TimeSpan.FromHours(19),
            Description = "Сделать уроки после учёбы"
        },
        new Note()
        {
            Name = "Сон",
            Time = TimeSpan.FromHours(23),
            Description = "Лечь спать в 23:00"
        }

    };
    public static Dictionary<DateTime, List<Note>> Calender = new Dictionary<DateTime, List<Note>>()
    {
        {DateTime.Now.AddDays(-10), Notes },
        {DateTime.Now.AddDays(-8), Notes },
        {DateTime.Now.AddDays(-6), Notes },
        {DateTime.Now.AddDays(-5), Notes },

    };

    public static void Main(string[] args)
    {
        State workState = State.MainMenu;
        NoteAction action = new NoteAction(NoteActionType.None, 0);
        List<DateTime> dates = null;
        DateTime currentDate = DateTime.Today;
        int currentDateIndex = 0;
        do
        {
            switch (workState)
            {
                case State.DateMenu:
                    ShowAllNotesByDate(currentDate);
                    action = CursorMover(Calender[currentDate].Count);
                    if (action.ActionType == NoteActionType.Enter)
                    {
                        workState = State.NoteRead;
                    }

                    if (action.ActionType == NoteActionType.LastDate)
                    {
                        workState = State.DateMenu;
                        currentDateIndex--;
                        if (currentDateIndex > 0 &&
                            currentDateIndex < dates.Count &&
                            dates[currentDateIndex] != null)
                        {
                            currentDate = dates[currentDateIndex];
                        }
                        else
                        {
                            currentDateIndex++;
                        }
                    }

                    if (action.ActionType == NoteActionType.NextDate)
                    {
                        workState = State.DateMenu;
                        currentDateIndex++;
                        if (currentDateIndex > 0 &&
                            currentDateIndex < dates.Count &&
                            dates[currentDateIndex] != null)
                        {
                            currentDate = dates[currentDateIndex];
                        }
                        else
                        {
                            currentDateIndex--;
                        }
                    }

                    if (action.ActionType == NoteActionType.Esc)
                    {
                        workState = State.MainMenu;
                    }
                    break;
                case State.MainMenu:
                    dates = ShowAllDates();
                    action = CursorMover(Calender.Count);
                    if (action.ActionType == NoteActionType.Enter)
                    {
                        workState = State.DateMenu;
                        currentDateIndex = action.Position - 1;
                        currentDate = dates[currentDateIndex];
                    }

                    if (action.ActionType == NoteActionType.Esc)
                    {
                        workState = State.CloseCalendar;
                        Console.Clear();
                    }
                    break;
                case State.NoteRead:
                    ShowNote(action.Position, currentDate);
                    action = WaitAction();
                    workState = State.DateMenu;
                    break;
                case State.NoteWrite:

                    break;
                case State.CloseCalendar:
                default:
                    Console.Clear();
                    break;
            }

        } while (workState != State.CloseCalendar);
    }

    private static void ShowNote(int actionPosition, DateTime currentDate)
    {
        Console.Clear();
        Note currentNote = Calender[currentDate][actionPosition - 1];
        Console.WriteLine(currentNote.Name + " " + currentDate.ToString("d") + " " + currentNote.Time);
        Console.WriteLine(new string('-', 80));
        Console.WriteLine(currentNote.Description);
    }

    static NoteAction CursorMover(int positions, int pos = 1)
    {
        ConsoleKeyInfo key;
        do
        {
            Console.SetCursorPosition(0, pos);
            Console.WriteLine("->");
            key = Console.ReadKey(true);
            Console.SetCursorPosition(0, pos);
            Console.WriteLine("  ");
            if (key.Key == ConsoleKey.UpArrow && pos != 1)
            {
                pos--;
            }
            else if (key.Key == ConsoleKey.DownArrow && pos != positions)
            {
                pos++;
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                return new NoteAction(NoteActionType.LastDate, 0);
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                return new NoteAction(NoteActionType.NextDate, 0);
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                return new NoteAction(NoteActionType.Esc, 0);
            }
        } while (key.Key != ConsoleKey.Enter);
        return new NoteAction(NoteActionType.Enter, pos);
    }
    static List<DateTime> ShowAllDates()
    {
        Console.Clear();
        Console.WriteLine("Дни с планами:");
        List<DateTime> dates = new List<DateTime>();
        foreach (var note in Calender)
        {
            Console.WriteLine("  " + note.Key.ToString("d"));
            dates.Add(note.Key);
        }
        return dates;
    }
    static void ShowAllNotesByDate(DateTime date)
    {
        Console.Clear();
        Console.WriteLine("Выбрана дата " + date.ToString("d"));
        foreach (Note note in Calender[date])
        {
            Console.WriteLine("  " + note.Name);
        }
    }

    static NoteAction WaitAction()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        do
        {
            key = Console.ReadKey(true);
        } while (key.Key != ConsoleKey.Escape);

        return new NoteAction(NoteActionType.Esc, 1);
    }
}




