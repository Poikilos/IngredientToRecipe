# IngredientToRecipe
When you find an item in Dragon Quest VIII, *minor mid-game spoiler* you can immediately know which items you can make in the Alchemy Pot.

Requirements:
- Download the guide(s) in the "requirements" directory to the "bin" folder (or same folder where you save IngredientToRecipe.exe.

My guide lists every ingredient (simple item, not advanced item) alphabetically, so that is how it is very different. Instead of looking up the item you want to make, you looking whatever random thing you got in a chest (or anywhere) and find out what you can make with it! Every ingredient is listed, not just the first ingredient. For example, if a recipe has 3 ingredients, the whole recipe is listed 3 times so you can look up each ingredient and still find the recipe by any one of the 3 item names! It helped me enjoy the game more by helping me get advanced items sooner so I could spend slightly less time grinding for EXP and gold.

You don't need to compile this old program that requires my older dependency. You can just download a release PDF:
- [Releases](https://github.com/Poikilos/IngredientToRecipe/releases).

This is a guide I made using some programming: I reversed three different guides and generated an HTML file. AIex' guide is not included because the author has not allowed redistribution of it. The program detected the recipes and ingredients, and I even made fixes to the guide. I hope you and others enjoy this guide, and I hope it helps you enjoy the game more.

Data was provided by GameWinners, RedScarlet, and AIex, with corrections and additions by Poikilos. Therefore this release is superior to the output of running IngredientToRecipe using those guides unedited as sources, which is possible in some cases. Releases are considered in good faith not to infringe upon any of the aforementioned previously copyrighted expression of the information, but rather to provide data about the game itself in a new expression (game rules cannot by copyrighted, only specific expressions of rules).

## Tasks
- [x] Load AIex alchemy recipes
- [x] Mark items as won by Arena Rank (by using  "ingredients-more.txt"; not in AIex's guide)
- [x] Mark items as won by Mini Medals (by using  "ingredients-more.txt" -- does not read AIex guide NUMBER OF MEDALS NEEDED section)
- [x] Mark items as obtained from Dodgy Dave
- [ ] Find a way to mark undesirable items easily (i.e. "danger" on EITHER line of description -- currently on the first line is checked)
