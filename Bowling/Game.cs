using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling
{
    public class Game
    {
        private const int RolesInRegularFrame = 2;
        private const int TotalFramesInGame = 10;
        private const int MaxRolesForGame = 21;
        private const int TotalPins = 10;
        private int _currentFrameIndex;
        private int _currentRollIndex;
        public Frame[] Frames { get; set; }


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

      
        public void Roll(int pins)
        {
            if (IsGameLastFrame())
            {
                Frames[_currentFrameIndex].Score += pins;
                ((LastSpecialFrame)(Frames[_currentFrameIndex])).ExtraRole = pins;

                return;
            }

            if (IsInvalidRoll())
            {
                throw new Exception("Cannot have extra role on last frame if not a spare or a strike");
            }

            Frames[_currentFrameIndex].Rolls[_currentRollIndex] = pins;
            Frames[_currentFrameIndex].Score = _currentRollIndex == 0 ? pins : Frames[_currentFrameIndex].Rolls.Sum();

            if (IsCurrentRollStrikeAndLast2RollsStrike())
            {
                Frames[_currentFrameIndex - 2].Score += TotalPins * 2;
            }
            else if (IsCurrentRollSpareAndLast2RollsStrike())
            {
                Frames[_currentFrameIndex - 2].Score += Frames[_currentFrameIndex - 1].Rolls[0] + pins;
            }
            else if (IsLastFrameStrikeAndEndOfCurrentFrame())
            {
                Frames[_currentFrameIndex - 1].Score += Frames[_currentFrameIndex].Rolls.Sum();
            }

            _currentRollIndex = ++_currentRollIndex % RolesInRegularFrame;

            if (_currentFrameIndex > 0)
            {
                var previousFrame = _currentFrameIndex - 1;

                if (IsEndOfCurrentFrameAndLastFrameSpare(previousFrame))
                {
                    Frames[previousFrame].Score += pins;
                }
            }

            if (NotLastFrame())
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
            else if (IsSpareOrStrikeOn2ndRoleOfFrame())
            {
                var lastSpecialFrame = new LastSpecialFrame();
                lastSpecialFrame.Rolls = Frames[_currentFrameIndex].Rolls;
                lastSpecialFrame.Score = Frames[_currentFrameIndex].Score;

                Frames[_currentFrameIndex] = lastSpecialFrame;
            }
        }

        private bool IsSpareOrStrikeOn2ndRoleOfFrame() => Frames[_currentFrameIndex].Score == TotalPins && _currentRollIndex != 1;
        
        private bool NotLastFrame() => (_currentFrameIndex + 1) != TotalFramesInGame;
        
        private bool IsEndOfCurrentFrameAndLastFrameSpare(int frame) => _currentRollIndex == 1 && IsFrameSpare(frame);

        private bool IsLastFrameStrikeAndEndOfCurrentFrame() => _currentFrameIndex > 0 && _currentRollIndex == 1 && Frames[_currentFrameIndex - 1].Rolls[0] == TotalPins;
        
        private bool IsCurrentRollSpareAndLast2RollsStrike() => _currentFrameIndex > 1 && _currentRollIndex == 0 && Frames[_currentFrameIndex - 2].Rolls[0] == TotalPins && Frames[_currentFrameIndex - 1].Rolls[0] == TotalPins;
        

        private bool IsCurrentRollStrikeAndLast2RollsStrike() => _currentFrameIndex > 1 && Frames[_currentFrameIndex].Rolls[0] == TotalPins && Frames[_currentFrameIndex - 2].Rolls[0] == TotalPins && Frames[_currentFrameIndex - 1].Rolls[0] == TotalPins;
        
        private bool IsInvalidRoll()=> _currentFrameIndex == (Frames.Length - 1) 
                                            && (Frames[Frames.Length - 1].Rolls.Sum() != TotalPins) 
                                            && ((Frames[Frames.Length - 1].Rolls.Sum() != (TotalPins * RolesInRegularFrame))) 
                                            && Frames[_currentFrameIndex].Score.HasValue && _currentRollIndex == 0;

        private bool IsGameLastFrame() => Frames[_currentFrameIndex] is LastSpecialFrame;

        private bool IsFrameSpare(int frameIndex) => !Frames[frameIndex].Rolls.Where(r => r == TotalPins).Any() && Frames[frameIndex].Score == TotalPins;

        public int CurrentFrame => _currentFrameIndex + 1;

        public int Score() => Frames.Sum(f => f.Score.GetValueOrDefault(0));     
    }
}
