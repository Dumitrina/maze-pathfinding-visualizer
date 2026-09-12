# Maze Pathfinding Visualizer

An interactive maze pathfinding visualizer developed in C# using Windows Forms.

The application uses a recursive backtracking algorithm to explore possible paths through a maze and visualizes the search process step by step.

## Features

- Dynamic maze visualization
- Recursive backtracking pathfinding
- Step-by-step path exploration
- Start and stop controls
- Solution counter
- Background processing to keep the user interface responsive
- Custom graphical resources for the mouse and destination

## Technologies

- C#
- .NET Framework
- Windows Forms
- Recursive Backtracking
- BackgroundWorker
- Visual Studio

## How It Works

The maze is loaded from an input file and displayed as a grid.

The algorithm recursively explores neighboring cells while avoiding walls and previously visited positions. The search process is sent back to the graphical interface, allowing each explored step to be visualized.

The application also keeps track of the number of valid paths found.

## Project Structure

- `MainForm.cs` – application logic and pathfinding algorithm
- `MainForm.Designer.cs` – Windows Forms interface definition
- `State.cs` – stores information used during the visualization
- `Program.cs` – application entry point
- `labirint.in` – maze input data
- `soarece.png` – mouse image
- `branza.png` – destination image

## What I Learned

This project helped me practice:

- Recursive algorithms
- Backtracking
- Windows Forms development
- Background processing
- Updating a graphical interface from a worker thread
- File input
- Organizing a C# desktop application

## Screenshot

![Maze Pathfinding Visualizer](screenshots/maze-demo.png)
