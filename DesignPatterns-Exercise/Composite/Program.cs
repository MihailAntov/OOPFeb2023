using Composite;
using System;


CompositeGift rootBox = new CompositeGift("Big box",10);
SingleGift phone = new SingleGift("Phone", 250);
SingleGift headphones = new SingleGift("Headphones", 15);
CompositeGift smallBox = new CompositeGift("Small box", 5);
SingleGift watch = new SingleGift("Watch", 400);
smallBox.Add(watch);
rootBox.Add(phone);
rootBox.Add(headphones);
rootBox.Add(smallBox);

Console.WriteLine($"The total price of the box is {rootBox.CalculateTotalPrice():f2}");
