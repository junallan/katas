using NUnit.Framework;
using System;

namespace Bowling.Tests
{
    public class BowlingTests
    {
        Game _game;

       [SetUp]
        public void Setup()
        {
            _game = new Game();
        }

        [Test]
        public void Bowling_score_for_no_pins_knocked_down()
        {
            _game.Roll(0);

            Assert.AreEqual(0, _game.Score());
        }

        [Test]
        public void Bowling_score_for_5_pins_knocked_down()
        {
            _game.Roll(5);

            Assert.AreEqual(5, _game.Score());
        }

        [Test]
        public void Bowling_score_for_9_pins_knocked_down()
        {  
           _game.Roll(9);

            Assert.AreEqual(9, _game.Score());
        }

        [Test]
        public void Bowling_score_for_two_roles_not_spare_or_strike()
        {
            _game.Roll(4);
            _game.Roll(5);

            Assert.AreEqual(9, _game.Score());
        }

        [Test]
        public void Bowling_score_for_spare()
        {
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(5);
            _game.Roll(2);

            Assert.AreEqual(22, _game.Score());
        }

        [Test]
        public void Bowling_score_for_strike()
        {
            _game.Roll(10);
            _game.Roll(5);
            _game.Roll(2);

            Assert.AreEqual(24, _game.Score());
        }

        [Test]
        public void Bowling_frame_for_no_role_is_0()
        {
            Assert.AreEqual(0, _game.Frame);
        }

        [Test]
        public void Bowling_frame_for_1_role_is_1()
        {
            _game.Roll(2);

            Assert.AreEqual(1, _game.Frame);
        }

        [Test]
        public void Bowling_frame_for_2_role_is_1()
        {
            _game.Roll(2);
            _game.Roll(3);

            Assert.AreEqual(1, _game.Frame);
        }

        [Test]
        public void Bowling_frame_for_3_role_is_2()
        {
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);

            Assert.AreEqual(2, _game.Frame);
        }

        [Test]
        public void Bowling_frame_for_4_role_is_2()
        {
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);

            Assert.AreEqual(2, _game.Frame);
        }

        [Test]
        public void Bowling_frame_for_5_role_is_2()
        {
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);
            _game.Roll(0);

            Assert.AreEqual(3, _game.Frame);
        }

        [Test]
        public void Bowling_for_game_is_20_roles_for_10_frames_if_non_spare_or_strike_on_last_frame()
        {
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);
            _game.Roll(0);
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);
            _game.Roll(0);
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);
            _game.Roll(0);
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);
            _game.Roll(0);

            Assert.AreEqual(10, _game.Frame);
        }

        [Test]
        public void Bowling_for_game_is_exception_if_over_20_roles_if_non_spare_or_strike_on_last_frame()
        {
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);
            _game.Roll(0);
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);
            _game.Roll(0);
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);
            _game.Roll(0);
            _game.Roll(2);
            _game.Roll(3);
            _game.Roll(7);
            _game.Roll(1);
            _game.Roll(0);
        
            var ex = Assert.Throws<Exception>(() => _game.Roll(0));
            Assert.That(ex.Message, Is.EqualTo("Cannot have extra role on last frame if not a spare or a strike"));
            //Assert.Throws<ArgumentException>(delegate { throw new ArgumentException(); });
        }

        [Test]
        public void Bowling_perfect_game_has_12_roles_and_10_frames()
        {
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);

            Assert.AreEqual(10, _game.Frame);
        }

        [Test]
        public void Bowling_perfect_game_with_10_roles()
        {
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);


            Assert.AreEqual(260, _game.Score());
        }

        [Test]
        public void Bowling_perfect_game_with_11_roles()
        {
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
     

            Assert.AreEqual(290, _game.Score());
        }

        [Test]
        public void Bowling_perfect_game_score()
        {
            _game.Roll(10);
            _game.Roll(10); 
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);

            Assert.AreEqual(300, _game.Score());
        }
    }
}