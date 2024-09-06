//// ConsoleTools
//// Copyright (C) 2017-2024 Dust in the Wind
//// 
//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
//// 
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

//using System;

//namespace DustInTheWind.ConsoleTools.Controls;

//public abstract class ControlRendererBase : IRenderer
//{
//    //private int currentLineLength;

//    public abstract bool HasMoreLines { get; }

//    //public int? MaxLineLength { get; set; }

//    //public int LineCount { get; protected set; }

//    public abstract void RenderNextLine();

//    //protected bool IsRoot { get; set; } = true;

//    protected IDisplay Display { get; private set; }

//    public virtual void Initialize(IDisplay display)
//    {
//        Display = display ?? throw new ArgumentNullException(nameof(display));
//    }

//    //protected void Write(string text, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
//    //{
//    //    if (text == null)
//    //        return;

//    //    int availableCharacterCount = MaxLineLength.HasValue
//    //        ? MaxLineLength.Value - currentLineLength
//    //        : int.MaxValue;

//    //    if (availableCharacterCount <= 0)
//    //        return;

//    //    string textToWrite = text.Length <= availableCharacterCount
//    //        ? text
//    //        : text.Substring(0, availableCharacterCount);

//    //    Display.DoWrite(textToWrite, foregroundColor, backgroundColor);

//    //    currentLineLength += textToWrite.Length;
//    //}

//    //protected void Write(char c, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
//    //{
//    //    int availableCharacterCount = MaxLineLength.HasValue
//    //        ? MaxLineLength.Value - currentLineLength
//    //        : int.MaxValue;

//    //    if (availableCharacterCount <= 0)
//    //        return;

//    //    Display.DoWrite(c, foregroundColor, backgroundColor);

//    //    currentLineLength++;
//    //}

//    //protected void WritePadding(int count)
//    //{
//    //    WriteSpaces(count, null, null);
//    //}

//    ////protected void WriteSpaces(int count, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
//    ////{
//    ////    int availableCharacterCount = MaxLineLength.HasValue
//    ////        ? MaxLineLength.Value - currentLineLength
//    ////        : int.MaxValue;

//    ////    int spacesCount = Math.Min(count, availableCharacterCount);

//    ////    if (spacesCount > 0)
//    ////    {
//    ////        string text = new(' ', spacesCount);
//    ////        Display.DoWrite(text, foregroundColor, backgroundColor);

//    ////        currentLineLength += spacesCount;
//    ////    }
//    ////}

//    ////protected void WriteLine(string text = null, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
//    ////{
//    ////    StartLine();
//    ////    Write(text, foregroundColor, backgroundColor);
//    ////    EndLine();
//    ////}

//    ////protected void StartLine()
//    ////{
//    ////    OnBeforeStartLine();
//    ////    OnAfterStartLine();
//    ////}

//    //protected virtual void OnBeforeStartLine()
//    //{
//    //}

//    //protected virtual void OnAfterStartLine()
//    //{
//    //}

//    //protected virtual void EndLine()
//    //{
//    //    OnBeforeEndLine();

//    //    int remainingLength = MaxLineLength.HasValue
//    //        ? MaxLineLength.Value - currentLineLength
//    //        : 0;

//    //    if (remainingLength > 0)
//    //    {
//    //        string text = new(' ', remainingLength);
//    //        Write(text);
//    //    }

//    //    if (IsRoot)
//    //        Display.DoWriteRootEndLine();

//    //    LineCount++;
//    //    currentLineLength = 0;

//    //    OnAfterEndLine();
//    //}

//    //protected virtual void OnBeforeEndLine()
//    //{
//    //}

//    //protected virtual void OnAfterEndLine()
//    //{
//    //}
//}