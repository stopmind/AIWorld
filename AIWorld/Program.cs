// See https://aka.ms/new-console-template for more information

using AIWorld.Scenes;
using Teko.Utils;
new GameBuilder()
    .StdServices(["Assets"], "game.log")
    .Window(16*80, 9*80, "AI World")
    .Scene(new BootstrapScene())
    .Run();