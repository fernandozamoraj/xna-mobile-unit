using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace XnaMobileUnit.Api
{
    public class TestsUi : DrawableGameComponent
    {
        private TestFixtureRunner _testFixtureRunner = new TestFixtureRunner();
        private SpriteFont _font;
        Vector2 _previousTouchPosition = new Vector2(0f, 0f);
        private float _yOffset;
        private float _xOffset;
        private float _fontHeight = 10;
        private List<TestLine> _testLines = new List<TestLine>();
        private float _widestLine;

        public TestsUi(Game game) : base(game)
        {
        }

        public void SetFont(SpriteFont font)
        {
            _font = font;
        }

        public void RunTests(params Assembly[] assemblies)
        {
            foreach(Assembly assembly in assemblies)
            {
                _testFixtureRunner.RunTests(assembly);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            TouchCollection touchCollection = TouchPanel.GetState();
            Vector2 touchPosition = new Vector2(_previousTouchPosition.X, _previousTouchPosition.Y);

            foreach (TouchLocation touchLocation in touchCollection)
            {
                touchPosition = touchLocation.Position;
            }

            float absoluteY = Math.Abs(touchPosition.Y - _previousTouchPosition.Y);
            float absoluteX = Math.Abs(touchPosition.X - _previousTouchPosition.X);

            if(absoluteY > absoluteX)
                ScrollVertically(touchPosition);
            else
                ScrollHorizontally(touchPosition);

            _previousTouchPosition = touchPosition;
        }

        private void ScrollVertically(Vector2 touchPosition)
        {
            if (_previousTouchPosition.Y < touchPosition.Y)
                _yOffset += _fontHeight/4;
            else if (_previousTouchPosition.Y > touchPosition.Y)
                _yOffset -= _fontHeight/4;

            //Don't allow to scroll off the page
            if (_yOffset < (-(_fontHeight * _testLines.Count))+100)
                _yOffset = (-(_fontHeight * _testLines.Count))+100;
            if (_yOffset > (_fontHeight * _testLines.Count)-100)
                _yOffset = (_fontHeight*_testLines.Count)-100;
        }

        private void ScrollHorizontally(Vector2 touchPosition)
        {
            if (_previousTouchPosition.X < touchPosition.X)
                _xOffset += _fontHeight/2; //FontHeight will do as a rule of thumb
            else if (_previousTouchPosition.X > touchPosition.X)
                _xOffset -= _fontHeight/2;

            //Don't allow to scroll off the page
            if (_xOffset < -(_widestLine/2 + _widestLine/3))
                _xOffset = -(_widestLine/2 + _widestLine/3);
            if (_xOffset > _widestLine/2 + _widestLine/3)
                _xOffset = _widestLine/2 + _widestLine/3;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = new SpriteBatch(base.Game.GraphicsDevice);

            _fontHeight = _font.MeasureString("W").Y + 5;

            int i = 1;

            if (_testLines == null || _testLines.Count == 0)
            {
                _testLines = new List<TestLine>();

                foreach (var line in _testFixtureRunner.GetTestExecutionTree())
                {
                    _testLines.Add(new TestLine{Line = line});
                    float lineWidth = _font.MeasureString(line).X;

                    if(lineWidth > _widestLine)
                        _widestLine = lineWidth;
                }
            }

            spriteBatch.Begin();

            foreach (var testLine in _testLines)
            {
                testLine.YPosition = (int)((i*_fontHeight) + _yOffset);
                if (testLine.YPosition > 0 && testLine.YPosition < 600)
                {
                    Color color = Color.Green;

                    if (testLine.Line.ToUpper().Contains("FAILED"))
                    {
                        color = Color.Red;
                    }

                    spriteBatch.DrawString(_font, testLine.Line, new Vector2(_xOffset, testLine.YPosition),
                       color);
                }
                i++;
            }

            spriteBatch.End();
        }
    }

    public class TestLine
    {
        public int YPosition;
        public string Line;
    }
}
