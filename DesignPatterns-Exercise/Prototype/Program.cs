using Prototype;
using System;

SandwichMenu menu = new SandwichMenu();
menu["BLT"] = new Sandwich("rye", "bacon", "", "tomato, lettuce");
menu["PBJ"] = new Sandwich("white", "", "", "peanut butter, jelly");

//insert more sandwiches

Sandwich order1 = menu["BLT"].Clone() as Sandwich;
Sandwich order2 = menu["BLT"].Clone() as Sandwich;


int a = 5;
