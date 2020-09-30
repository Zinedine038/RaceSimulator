using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace TestProject
{
    [TestFixture]
    class Model_Competition_NextTrackShould
    {
        private Competition _competition;
        
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();
            Assert.IsNull(result);  
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track track = new Track("Silverstone");
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();
            Assert.AreEqual(track, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track track = new Track("Silverstone");
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();
            result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track trackX = new Track("Silverstone");
            _competition.Tracks.Enqueue(trackX);
            var result = _competition.NextTrack();
            Assert.AreEqual(trackX, result);
            Track trackY = new Track("Zandvoort");
            _competition.Tracks.Enqueue(trackY);
            result = _competition.NextTrack();
            Assert.AreEqual(trackY, result);

        }


    }
}
