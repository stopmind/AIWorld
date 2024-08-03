// See https://aka.ms/new-console-template for more information

using AIWorld.Scenes;
using Teko.Utils;
new GameBuilder()
    .StdServices(["Assets"], "game.log")
    .Window(1200, 675, "AI World")
    .Scene(new BootstrapScene())
    .Run();