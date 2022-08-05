namespace TUI;

public abstract class Element
{
    private int PositionLeft, PositionTop;
    private int oldPositionLeft, oldPositionTop;

    public Element(bool isNewLine = true, bool isEndl = true)
    {

        if (isNewLine) Console.Write('\n');

        PositionLeft = 0;
        PositionTop = Console.CursorTop;

        Draw();

        if (isEndl) Console.Write('\n');
    }

    public void DrawRun()
    {
        DrawStart();
        Draw();
        DrawEnd();
    }

    private void DrawStart()
    {
        oldPositionLeft = Console.CursorLeft;
        oldPositionTop = Console.CursorTop;

        Console.SetCursorPosition(PositionLeft, PositionTop);
    }

    private void DrawEnd()
    {
        Console.SetCursorPosition(oldPositionLeft, oldPositionTop);
    }

    protected abstract void Draw();
}