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

- Endurance (Physical Durability)
- Speed
- Strength (Brute, pure strength - can it lift a car? can it destroy a building with a punch?)
- Intellect (Intelligence)
- Fire Power (How this character uses its abilities: maybe the character is weak in Strength, but their whole abilites focus on being a huge influencer, or magic. Hulk is a character with high Strength and low Fire Power, meanwhile Zatanna is a character with low Strength but high Fire Power)

We calculate the average of these stats and apply a multiplier based on the selected menace level.

The "Menace Level" is an enum, and it works like this:

- Coward (0.5x): a character that is known by fleeing battles. I.e: Spandam (One Piece)
- Pacifist (0.25x): a character that usually refuses to fight; it can fight, it can be strong, but USUALLY their personal choice is not to fight. I.e: Aang (Avatar)
- Normal (1x): self-explanatory tbh
- Aggressive (1.25x): a character that is known to being aggressive first - it is not a crazy dog, always searching for chaos and destruction, as it can be a reserved character. BUUT, whenever they're fighting, they go all-in. I.e: Kratos (God of War)
- Homicidal (1.5x): a character that isn't rational, or at least a completely crazy person. it works as a chaos agent, bring death and destruction wherever they pass. I.e: Carnage

At the end, the calculus will be:

(Endurance + Speed + Strength + Intellect + Fire Power / 5) * Menace Multiplier. 