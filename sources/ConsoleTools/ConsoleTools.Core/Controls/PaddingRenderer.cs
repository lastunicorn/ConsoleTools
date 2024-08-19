namespace DustInTheWind.ConsoleTools.Controls;

internal class PaddingRenderer : IRenderer
{
    private int actualCount;

    public int PaddingLeft { get; set; }

    public int PaddingRight { get; set; }

    public int ContentWidth { get; set; }

    public int Height { get; set; }

    public bool HasMoreLines => actualCount < Height;

    public void RenderNextLine(IDisplay display)
    {
        if (actualCount >= Height)
            return;

        if (PaddingLeft > 0)
        {
            string text = new(' ', PaddingLeft);
            display.Write(text);
        }

        if (ContentWidth > 0)
        {
            string text = new(' ', ContentWidth);
            display.Write(text);
        }

        if (PaddingRight > 0)
        {
            string text = new(' ', PaddingRight);
            display.Write(text);
        }

        actualCount++;
    }
}