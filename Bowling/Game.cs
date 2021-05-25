using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling
{
    public class Game
    {
        private int _score = 0;
        private int _roleTotal = 0;
        private int? _previousRoleScore = null;
        private int? _previousPreviousRoleScore = null;
        private int _rolesInLastFrame = 0;

        private const int RolesInRegularFrame = 2;
        private const int TotalFramesInGame = 10;
        private const int MaxRolesForGame = 21;
        private const int TotalPins = 10;

        private Frame[] _frames;
        private int _currentFrameIndex;
        private int _currentRollIndex;


        public class Frame
        {
            public int[] Rolls = new int[2];
            public int Score;
        }

        public class LastSpecialFrame : Frame
        {
            public int ExtraRole;
        }


        public Game()
        {
            _frames = new Frame[10] { new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame() };
        }

        public void Roll(int pins)
        {
            _frames[_currentFrameIndex].Rolls[_currentRollIndex] = pins;
            _frames[_currentFrameIndex].Score = _currentRollIndex == 0 ? pins : _frames[_currentFrameIndex].Rolls.Sum();

           _currentRollIndex = ++_currentRollIndex % RolesInRegularFrame; 

            if(_currentFrameIndex > 0)
            {
                var previousFrame = _currentFrameIndex - 1;
                if (_currentRollIndex == 1)
                {
                    if (IsFrameSpare(previousFrame))
                    {
                        _frames[previousFrame].Score += pins;
                    }
                }
                else
                {
                    if (IsFrameStrike(previousFrame))
                    {
                        _frames[previousFrame].Score += _frames[_currentFrameIndex].Score;
                    }
                }
            }
            if(_currentRollIndex == 0)
            {
                _currentFrameIndex++;
            }

            if (pins == TotalPins)
            {
                _currentFrameIndex++;
                _currentRollIndex = 0;
            }

            //_roleTotal++;

            //if (_roleTotal == MaxRolesForGame && !Last2PinsScoreTotalIs(TotalPins))
            //{
            //    throw new Exception("Cannot have extra role on last frame if not a spare or a strike");
            //}

            //if(_currentFrame < TotalFramesInGame)
            //{
            //    if (pins == TotalPins)
            //    {
            //        _currentFrame++;
            //    }
            //    else if (_roleTotal % RolesInFrame != 0)
            //    {
            //        _currentFrame++;
            //    }
            //}

            //if(_currentFrame == TotalFramesInGame)
            //{
            //    _rolesInLastFrame++;


            //}


            //if (!(_currentFrame == TotalFramesInGame && _rolesInLastFrame == 3))
            //{         
            //    if (IsPreviousPreviousRoleStrike())
            //    {
            //        _score += _previousRoleScore.Value + pins;
            //    }
            //    else if(IsLastRolesSpare())
            //    {
            //        _score += pins;
            //    }
            //}

            //_score += pins;

            //if (_roleTotal == 1)
            //{
            //    _previousRoleScore = pins;
            //}

            //if (_roleTotal > 1)
            //{
            //    _previousPreviousRoleScore = _previousRoleScore;
            //    _previousRoleScore = pins;
            //}
        }

        private bool IsFrameStrike(int previousFrame)
        {
            return _frames[previousFrame].Rolls.Where(r => r == TotalPins).Any();
        }

        private bool IsFrameSpare(int previousFrame)
        {
            return !_frames[previousFrame].Rolls.Where(r => r == TotalPins).Any() && _frames[previousFrame].Score == TotalPins;
        }

        //private bool IsNewFrame()
        //{
        //    return (_roleTotal % RolesInFrame == 0);
        //}

        //private bool IsPreviousPreviousRoleStrike()
        //{
        //    return _previousPreviousRoleScore.HasValue && _previousPreviousRoleScore.Value == TotalPins;
        //}

        //private bool IsLastRolesSpare()
        //{
        //    return  _roleTotal % RolesInFrame != 0
        //            && _previousPreviousRoleScore.HasValue && _previousRoleScore.HasValue 
        //            && ((_previousPreviousRoleScore.Value + _previousRoleScore.Value) == TotalPins);
        //}

        //private bool Last2PinsScoreTotalIs(int scoreTotal)
        //{
        //    return _previousRoleScore.HasValue && _previousPreviousRoleScore.HasValue && ((_previousRoleScore.Value + _previousPreviousRoleScore.Value) == scoreTotal);
        //}

        public int Score()
        {
            return _frames.Sum(f => f.Score);
        }
    }
}
