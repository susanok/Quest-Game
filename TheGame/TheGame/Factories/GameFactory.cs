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

                //// TODO: add more quests
                List<Position> bombPositions = new List<Position>();

                var questNames = new List<string>
                {
                    "QuizQuest",
                    "FallingRocks"
                };
                var questsCount = questNames.Count;

                for (int j = 0; j < 4; j++)
                {
                    var xRand1 = 0;
                    var xRand2 = 0;
                    var yRand1 = 0;
                    var yRand2 = 0;
                    switch (j)
                    {
                        case 0:
                            xRand1 = 1;
                            xRand2 = innerBoxStartingWidthCoo - 1;
                            yRand1 = 1;
                            yRand2 = innerBoxStartingHeightCoo - 1;
                            break;
                        case 1:
                            xRand1 = innerBoxStartingWidthCoo + innerBoxPreferedWidth + 1;
                            xRand2 = Constants.BoxWidth - 1;
                            yRand1 = 1;
                            yRand2 = innerBoxStartingHeightCoo - 1;
                            break;
                        case 2:
                            xRand1 = 1;
                            xRand2 = innerBoxStartingWidthCoo - 1;
                            yRand1 = innerBoxStartingHeightCoo + innerBoxPreferedHeight + 1;
                            yRand2 = Constants.BoxHeight - 1;
                            break;
                        case 3:
                            xRand1 = innerBoxStartingWidthCoo + innerBoxPreferedWidth + 1;
                            xRand2 = Constants.BoxWidth - 1;
                            yRand1 = innerBoxStartingHeightCoo + innerBoxPreferedHeight + 1;
                            yRand2 = Constants.BoxHeight - 1;
                            break;
                        default:
                            break;
                    }
                    
                    for (int i = 0; i < 3; i++)
                    {
                        IDisplayPiece quizQuest = QuestFactory.GetQuest(questNames[Generator.GetRandomNumber(0, questsCount)], this.UseCurrentID(), Generator.GetRandomNumber(xRand1, xRand2), Generator.GetRandomNumber(yRand1,yRand2));
                        game.AddBoardElement(quizQuest);
                    }
                    for (int k = 0; k < 5; k++)
                    {
                        Position bombPosition = new Position(Generator.GetRandomNumber(xRand1, xRand2), Generator.GetRandomNumber(yRand1, yRand2));
                        bombPositions.Add(bombPosition);
                        Bomb bomb = new Bomb(bombPositions, Generator.GetRandomNumber(1,4));
                        bomb.SetID(this.UseCurrentID());
                        game.AddBoardElement(bomb);
                    }
                }
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