# PowerScale API

A simple API to compare fictional characters based on their attributes and "menace level".

Nothing too fancy, just a clean backend to simulate who would win in a fight. Use it to play with your friends, idk

---

## ⚙️ Tech Stack

- ASP.NET Core
- Entity Framework Core
- SQL Server

---

## 🧠 How it works

Each character has base attributes:

- Endurance
- Speed
- Strength
- Intellect

We calculate the average of these stats and apply a multiplier based on the selected menace level (For example, Aang is a pacifist character. A pacifist, by default, has a multiplier of 0.25x on their final results. An homicide character, like Joker, would have a multiplier of 1.5x. Don't take it too serious, tho. They are enums and can be easily changed or removed).
