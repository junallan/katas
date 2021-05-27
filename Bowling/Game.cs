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

        public Frame[] Frames { get; set; }
        private int _currentFrameIndex;
        private int _currentRollIndex;


        public class Frame
        {
            public int[] Rolls = new int[2];
            public int? Score;
        }

        public class LastSpecialFrame : Frame
        {
            public int ExtraRole;
        }


        public Game()
        {
            Frames = new Frame[10] { new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame(), new Frame() };
        }

        public int CurrentFrame => _currentFrameIndex + 1;

        public void Roll(int pins)
        {
            if (_currentFrameIndex == (Frames.Length - 1) && (Frames[Frames.Length - 1].Rolls.Sum() != TotalPins) && ((Frames[Frames.Length - 1].Rolls.Sum() != (TotalPins* RolesInRegularFrame))) && Frames[_currentFrameIndex].Score.HasValue && _currentRollIndex == 0)
            {
                throw new Exception("Cannot have extra role on last frame if not a spare or a strike");
            }

            Frames[_currentFrameIndex].Rolls[_currentRollIndex] = pins;
            Frames[_currentFrameIndex].Score = _currentRollIndex == 0 ? pins : Frames[_currentFrameIndex].Rolls.Sum();

           _currentRollIndex = ++_currentRollIndex % RolesInRegularFrame; 

            if(_currentFrameIndex > 0)
            {
                var previousFrame = _currentFrameIndex - 1;
                if (_currentRollIndex == 1)
                {
                    if (IsFrameSpare(previousFrame))
                    {
                        Frames[previousFrame].Score += pins;
                    }
                    //else if (IsFrameStrike(previousFrame))
                    //{
                    //    if(curre)
                    //     && Frames[previousFrame].Score == TotalPins && pins == TotalPins
                    //    Frames[previousFrame].Score += Frames[_currentFrameIndex].Score;
                    //}
                }
                else
                {
                    if (IsFrameStrike(previousFrame))
                    {
                        Frames[previousFrame].Score += Frames[_currentFrameIndex].Score;
                    }
                }
            }

            if((_currentFrameIndex + 1) != TotalFramesInGame)
            {
                if (_currentRollIndex == 0)
                {
                    _currentFrameIndex++;
                }

                if (pins == TotalPins)
                {
                    _currentFrameIndex++;
                    _currentRollIndex = 0;
                }
            }



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

        private bool IsFrameStrike(int frameIndex)
        {
            return Frames[frameIndex].Rolls.Where(r => r == TotalPins).Any();
        }

        private bool IsFrameSpare(int frameIndex)
        {
            return !Frames[frameIndex].Rolls.Where(r => r == TotalPins).Any() && Frames[frameIndex].Score == TotalPins;
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
            return Frames.Sum(f => f.Score.GetValueOrDefault(0));
        }
    }
}
