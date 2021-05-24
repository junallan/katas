using System;
using System.Collections.Generic;
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

        private const int RolesInFrame = 2;
        private const int TotalFramesInGame = 10;
        private const int MaxRolesForGame = 21;
        private const int TotalPins = 10;

        public int Frame { get; set; }


        public void Roll(int pins)
        {
            _roleTotal++;

            if (_roleTotal == MaxRolesForGame && !Last2PinsScoreTotalIs(TotalPins))
            {
                throw new Exception("Cannot have extra role on last frame if not a spare or a strike");
            }

            if(Frame < TotalFramesInGame)
            {
                if (pins == TotalPins)
                {
                    Frame++;
                }
                else if (_roleTotal % RolesInFrame != 0)
                {
                    Frame++;
                }
            }

            if(Frame == TotalFramesInGame)
            {
                _rolesInLastFrame++;

             
            }


            if (!(Frame == TotalFramesInGame && _rolesInLastFrame == 3))
            {         
                if (IsPreviousPreviousRoleStrike())
                {
                    _score += _previousRoleScore.Value + pins;
                }
                else if(IsLastRolesSpare())
                {
                    _score += pins;
                }
            }

            _score += pins;

            if (_roleTotal == 1)
            {
                _previousRoleScore = pins;
            }

            if (_roleTotal > 1)
            {
                _previousPreviousRoleScore = _previousRoleScore;
                _previousRoleScore = pins;
            }
        }

        private bool IsNewFrame()
        {
            return (_roleTotal % RolesInFrame == 0);
        }

        private bool IsPreviousPreviousRoleStrike()
        {
            return _previousPreviousRoleScore.HasValue && _previousPreviousRoleScore.Value == TotalPins;
        }

        private bool IsLastRolesSpare()
        {
            return  _roleTotal % RolesInFrame != 0
                    && _previousPreviousRoleScore.HasValue && _previousRoleScore.HasValue 
                    && ((_previousPreviousRoleScore.Value + _previousRoleScore.Value) == TotalPins);
        }

        private bool Last2PinsScoreTotalIs(int scoreTotal)
        {
            return _previousRoleScore.HasValue && _previousPreviousRoleScore.HasValue && ((_previousRoleScore.Value + _previousPreviousRoleScore.Value) == scoreTotal);
        }

        public int Score()
        {
            return _score;
        }
    }
}
