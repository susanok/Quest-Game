﻿namespace TheGame.Factories
{
    using System.Collections.Generic;
    using TheGame.BoardInterfaces;
    using TheGame.BoardPieces;
    using TheGame.BoardPieces.Units;
    using TheGame.Games;
    using TheGame.Helpers;
    using TheGame.Utils;

    public class GameFactory
    {
        private int idCounter = 0;

        public IGame GetGame(string gameName)
        {
            if (gameName == "MainGame")
            {
                MainGame game = new MainGame();
                Position outerBoxStarterPosition = new Position(0, 0);
                Box outerBorder = new Box(outerBoxStarterPosition, Constants.BoxWidth, Constants.BoxHeight);
                outerBorder.SetID(this.UseCurrentID());
                game.AddBoardElement(outerBorder);

                //// TODO: implement abstract logic for this
                int outerBoxWidthMiddle = Constants.BoxWidth / 2;
                int outerBoxHeightMiddle = Constants.BoxHeight / 2;
                int innerBoxPreferedWidth = Constants.BoxWidth / 10;
                int innerBoxPreferedHeight = Constants.BoxHeight / 10;
                int innerBoxStartingWidthCoo = outerBoxWidthMiddle - (innerBoxPreferedWidth / 2);
                int innerBoxStartingHeightCoo = outerBoxHeightMiddle - (innerBoxPreferedHeight / 2);

                Position innerBoxStarterPosition = new Position(innerBoxStartingWidthCoo, innerBoxStartingHeightCoo);

                Box innerBox = new Box(innerBoxStarterPosition, innerBoxPreferedWidth, innerBoxPreferedHeight);
                innerBox.SetID(this.UseCurrentID());
                game.AddBoardElement(innerBox);
                game.SetInitPositionOfBorderAroundWinArea(innerBoxStarterPosition);

                Position winAreaTopLeft = new Position(innerBoxStarterPosition.GetWidthCoo() + 1, innerBoxStarterPosition.GetDebthCoo() + 2);
                int winAreaWidth = innerBoxPreferedWidth - 2;
                int winAreaDebth = innerBoxPreferedHeight - 2;

                IDisplayPiece winArea = WinAreaFactory.GetWinArea(winAreaTopLeft, winAreaWidth, winAreaDebth);
                game.AddBoardElement(winArea);

                Position playerStartingPosition = new Position(Constants.PlayerStartingX, Constants.PlayerStartingY);
                List<Position> playerPosition = new List<Position>();
                playerPosition.Add(playerStartingPosition);
                Priest player = new Priest(playerPosition);
                player.SetID(this.UseCurrentID());

                game.AddBoardElement(player);

                //// TODO: Randomize bombs/quests
                //// TODO: add more bombs/quests at random
                Position bombPosition = new Position(10, 10);
                List<Position> bombPositions = new List<Position>();
                bombPositions.Add(bombPosition);
                Bomb bomb = new Bomb(bombPositions, 1);
                bomb.SetID(this.UseCurrentID());
                game.AddBoardElement(bomb);

                IDisplayPiece quizQuest1 = QuestFactory.GetQuest("QuizQuest", this.UseCurrentID(), 11, 11);
                IDisplayPiece quizQuest2 = QuestFactory.GetQuest("QuizQuest", this.UseCurrentID(), 12, 12);
                game.AddBoardElement(quizQuest1);
                game.AddBoardElement(quizQuest2);

                game.SetMinimumWinScore(2);

                return game;
            }

            IGame game1 = null;

            return game1;
        }

        private int UseCurrentID()
        {
            int currentId = this.idCounter;
            this.idCounter++;

            return currentId;
        }
    }
}